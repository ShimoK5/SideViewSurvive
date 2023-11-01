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

    [Header("スタートアニメーションのフレーム数")]
    [SerializeField] int StartMovieFlame;
    int StartMovieFlameCount = 0;

    [Header("死亡アニメーションのフレーム数")]
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

        float TanTheta = Mathf.Tan(51.225f * Mathf.Deg2Rad);
        float DistanceZ = Mathf.Abs(transform.position.z - MinObj.z);//プレイヤーの壁をメイン
        //float DistanceZ = Mathf.Abs(transform.position.z - (-1));//ますをメイン
        float Zurashi = TanTheta * DistanceZ  -2.5f;

        MinX = MinObj.x + Zurashi;
        MaxX = MaxObj.x - Zurashi;

        instance = this;
        //targetオブジェクトを取得

        ZoomCamera = false;

        targetObj = GameObject.Find("Player");
        FollowObj();

        StartMovieInit();
    }

    public void FixedUpdate()
    {
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
            transform.position = new Vector3(targetObj.transform.position.x /*+ ZurashiX*/,
            transform.position.y,
            transform.position.z);
        }
        else
        {
            transform.position = new Vector3(targetObj.transform.position.x /*+ ZurashiX*/,
            targetObj.transform.position.y + cameraPosY,
            transform.position.z);
        }

        if(transform.position.x <= MinX)
        {
            transform.position = new Vector3(MinX, transform.position.y, transform.position.z);
        }
        if(transform.position.x >= MaxX)
        {
            transform.position = new Vector3(MaxX, transform.position.y, transform.position.z);
        }
    }

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


