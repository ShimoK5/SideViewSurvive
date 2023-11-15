using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraPos : MonoBehaviour
{

    [Header("Y固定")]
    [SerializeField] bool LockY;

    //[Header("Xずらし")]
    //[SerializeField] float ZurashiX;

    public static CameraPos instance;
    private GameObject targetObj;//targetオブジェクトデータ
    public float cameraPosY = 5.3f;//Y座標の加算値

    float MinX;         //カメラの最低X座標
    float MaxX;         //カメラの最高X座標

    Vector3 DefaultPos = Vector3.zero;  //正常な位置
    Vector3 SwingAddValue = Vector3.zero;    //カメラ座標に加算する値（敵ヒット時など）
    int HitSwingCnt = 0;
    Vector3 FirstSwingVector = Vector3.zero;
    bool bHitSwing = false;

    public float ViewWidth;     //映してる領域の横幅
    public float ViewHeight;    //映してる領域の縦幅

    [Header("ヒット振動距離の長さ")]
    [SerializeField] float MAX_SWING_RANGE ;
    [Header("ヒット振動時間の長さ")]
    [SerializeField] int MAX_SWING_CNT; 

    [Header("スタートアニメーションのフレーム数")]
    [SerializeField] int StartMovieFlame;
    int StartMovieFlameCount = 0;

    [Header("死亡アニメーションのカメラ近づきフレーム数")]
    [SerializeField] int DeadMovieFlame;
    int DeadMovieFlameCnt = 0;
    Vector3 DeadPlayerFirstCameraPos;

    [Header("ゴールアニメーションのカメラ近づきフレーム数")]
    [SerializeField] int GoalMovieFlame;
    int GoalMovieFlameCnt = 0;
    Vector3 GoalPlayerFirstCameraPos;
    public bool ZoomCamera = false;


    void Start()
    {
        Vector3 MinObj = GameObject.Find("StartObj").transform.position;
        Vector3 MaxObj = GameObject.Find("GoalObj").transform.position;

        //視野角の半分のTanを保存
        float TanThetaX = Mathf.Tan(VerticalToHorizontalFov(GetComponent<Camera>().fieldOfView, GetComponent<Camera>().aspect) * 0.5f * Mathf.Deg2Rad);
        float TanThetaY = Mathf.Tan(GetComponent<Camera>().fieldOfView * 0.5f * Mathf.Deg2Rad);
        //カメラ座標Zからプレイヤーが立つ中心位置のZの距離
        float DistanceZ = Mathf.Abs(transform.position.z - MinObj.z);//プレイヤーの壁をメイン
        //カメラ座標Zからブロックの手前座標Zの距離
        //float DistanceZ = Mathf.Abs(transform.position.z - (-1));//ますをメイン
        
        //映してる領域を保存
        ViewWidth = TanThetaX * DistanceZ * 2;
        ViewHeight = TanThetaY * DistanceZ * 2;

        //MinXとMaxXobjからどれだけ離した位置で止めるのかの距離保存
        float ZurashiX = TanThetaX * DistanceZ  -2.5f;

        MinX = MinObj.x + ZurashiX;
        MaxX = MaxObj.x - ZurashiX;

        instance = this;
        
        ZoomCamera = false;
        //targetオブジェクトを取得
        targetObj = GameObject.Find("Player");

        DefaultPos = transform.position;

        FollowObj();

        StartMovieInit();
    }

    private float VerticalToHorizontalFov(float horizontalFov, float aspectRatio)
    {
        return 2f * Mathf.Rad2Deg *  Mathf.Atan(Mathf.Tan(horizontalFov * 0.5f * Mathf.Deg2Rad) * aspectRatio);
    }

    public void FixedUpdate()
    {
        //GameObject myPrefab = (GameObject)Resources.Load("CubeParent");//プレハブをGameObject型で取得
        //GameObject Obj = Instantiate(myPrefab, new Vector3 (transform.position.x, transform.position.y,Player.instance.transform.position.z)  + new Vector3(ViewWidth * 0.5f, ViewHeight * 0.5f, 0), Quaternion.identity);
        //GameObject Obj2 = Instantiate(myPrefab, new Vector3 (transform.position.x, transform.position.y,Player.instance.transform.position.z)  - new Vector3(ViewWidth * 0.5f, ViewHeight * 0.5f, 0), Quaternion.identity);
        //GameObject Obj3 = Instantiate(myPrefab, new Vector3 (transform.position.x, transform.position.y,Player.instance.transform.position.z)  - new Vector3(0,0, 0), Quaternion.identity);

        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.StartCameraMotion:
                StartMovieUpdate();
                break;

            case GAME_STATE.Game:

                FollowObj();
                //別ステートで使う座標保存
                DeadPlayerFirstCameraPos = transform.position;
                GoalPlayerFirstCameraPos = transform.position;
                break;

            case GAME_STATE.DeadPlayer:
                DeadPlayerUpdate();
                break;

            case GAME_STATE.EndPlayerMotion:
                EndMovieUpdate();
                break;

        }
    }

    public void FollowObj()
    {
        if(LockY)
        {
            DefaultPos = new Vector3(targetObj.transform.position.x /*+ ZurashiX*/,
            DefaultPos.y,
            DefaultPos.z);
        }
        else
        {
            DefaultPos = new Vector3(targetObj.transform.position.x /*+ ZurashiX*/,
            targetObj.transform.position.y + cameraPosY,
            DefaultPos.z);
        }

        if(DefaultPos.x <= MinX)
        {
            DefaultPos = new Vector3(MinX, DefaultPos.y, DefaultPos.z);
        }
        if(DefaultPos.x >= MaxX)
        {
            DefaultPos = new Vector3(MaxX, DefaultPos.y, DefaultPos.z);
        }

        //ヒット振動の更新
        HitSwingUpdate();

        //適用
        transform.position = DefaultPos + SwingAddValue;
    }

    public void HitSwing(Vector3 direction)
    {
        bHitSwing = true;
        HitSwingCnt = 0;
        FirstSwingVector = direction.normalized;
    }

    //ヒット振動の更新
    void HitSwingUpdate()
    {
        if (bHitSwing)
        {
            HitSwingCnt++;

            //　Temp = 6 5 4 3 2 1 0 
            int Temp = MAX_SWING_CNT - HitSwingCnt;

            //大きさ
            float SwingRange = Temp * MAX_SWING_RANGE;

            //大きさと正規化向きをかける
            if(HitSwingCnt == 1)
            {
                SwingAddValue = FirstSwingVector.normalized * SwingRange;
            }
            else
            {
                float Angle = Random.Range(0.0f, 6.28f);
                Vector3 SwingVector = Vector3.zero;
                SwingVector.x = Mathf.Cos(Angle);
                SwingVector.y = Mathf.Sin(Angle);
                SwingAddValue = SwingVector.normalized * SwingRange;
            }
            

            //時間が終わったら
            if (HitSwingCnt >= MAX_SWING_CNT)
            {
                HitSwingCnt = 0;
                bHitSwing = false;
                SwingAddValue = Vector3.zero;
            }
        }
    }


    //void HitSwingUpdate()
    //{
    //    if(bHitSwing)
    //    {
    //        HitSwingCnt++;

    //        //　Temp = 0 1 2 3 2 1 0
    //        int Temp;
    //        if(HitSwingCnt <= MAX_SWING_CNT / 2)
    //        {
    //            Temp = HitSwingCnt;
    //        }
    //        else
    //        {
    //            Temp = MAX_SWING_CNT - HitSwingCnt;
    //        }
    //        //大きさ
    //        float SwingRange = Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, Temp, MAX_SWING_CNT / 2, 0, MAX_SWING_RANGE);
    //        //大きさと正規化向きをかける
    //        SwingAddValue = FirstSwingVector.normalized * SwingRange;

    //        //時間が終わったら
    //        if (HitSwingCnt >= MAX_SWING_CNT)
    //        {
    //            HitSwingCnt = 0;
    //            bHitSwing = false;
    //            SwingAddValue = Vector3.zero;
    //        }
    //    }
    //}

    void StartMovieUpdate()
    {
        StartMovieFlameCount++;

        if(InputManager_FU.instanse.GetKey(Key.B))
        {
            StartMovieFlameCount += 3;
        }

        float PosX = Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, StartMovieFlameCount, StartMovieFlame, MaxX, MinX);

        transform.position = new Vector3(PosX, transform.position.y, transform.position.z);

        if (StartMovieFlameCount >= StartMovieFlame)
        {
            GameStateManager.instance.GameState = GAME_STATE.Game;
            transform.position = new Vector3(MinX, transform.position.y, transform.position.z);
            //スタート演出

        }
    }

    void StartMovieInit()
    {
        transform.position = new Vector3(MaxX /*+ ZurashiX*/,
            targetObj.transform.position.y + cameraPosY,
            transform.position.z);
    }

    void DeadPlayerUpdate()
    {
        DeadMovieFlameCnt++;

        Vector3 GoPos = new Vector3(targetObj.transform.position.x,
            targetObj.transform.position.y , - 3);

        float NowWariai = Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, DeadMovieFlameCnt, DeadMovieFlame, 0.0f, 1.0f);

        Vector3 NowPos = Vector3.Lerp(DeadPlayerFirstCameraPos, GoPos, NowWariai);

        transform.position = NowPos;

        if(DeadMovieFlameCnt >= DeadMovieFlame)
        {
            transform.position = GoPos;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void EndMovieUpdate()
    {
        if(ZoomCamera)
        {
            GoalMovieFlameCnt++;

            Vector3 GoPos = new Vector3(targetObj.transform.position.x,
                targetObj.transform.position.y, -3);

            float NowWariai = Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, GoalMovieFlameCnt, GoalMovieFlame, 0.0f, 1.0f);

            Vector3 NowPos = Vector3.Lerp(GoalPlayerFirstCameraPos, GoPos, NowWariai);

            transform.position = NowPos;

            if (GoalMovieFlameCnt >= GoalMovieFlame)
            {
                transform.position = GoPos;
            }
        }
    }
}


