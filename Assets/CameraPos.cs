using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float PosX = transform.position.x - 0.1f;

        transform.position = new Vector3(PosX, transform.position.y, transform.position.z);

        if (PosX < MinX)
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
}


