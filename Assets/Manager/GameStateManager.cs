using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE { 
StartFade,          //開始フェード
StartCameraMotion,  //スタートカメラ移動
StartPlayerMotion,  //プレイヤースタート演出  
Game,               //ゲーム
Pause,              //ポーズ
DeadPlayerStop,     //プレイヤー死亡時固定
DeadPlayer,         //プレイヤー死亡時演出
EndPlayerMotion,    //プレイヤーゴール演出
EndFade,            //終了フェード
}


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GAME_STATE GameState;        //インスペクターから初期値を選択

    int DeadPlayerStopCnt;

    GameObject UI_Canvas;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UI_Canvas = GameObject.Find("UI_Canvas");
    }

    void FixedUpdate()
    {
        switch (GameState)
        {
            case GAME_STATE.DeadPlayerStop:
                //カウント加算
                DeadPlayerStopCnt++;
                //アニメーションストップ
                Player.instance.GetAnim().Anim.timeScale = 0;
                
                if(UI_Canvas.activeSelf == true)
                {
                    //UIキャンバス非表示
                    UI_Canvas.SetActive(false);
                    //プレハブ生成
                    GameObject Canvas = (GameObject)Resources.Load("DeadEffectCanvas");
                    Canvas = Instantiate(Canvas,Vector3.zero, Quaternion.Euler(Vector3.zero));
                }
                   

                //25フレ止めた後
                if (DeadPlayerStopCnt > 25)
                {
                    //アニメーション再開
                    Player.instance.GetAnim().Anim.timeScale = 1;
                    //ゲームステート変更
                    GameState = GAME_STATE.DeadPlayer;
                }
               

                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
