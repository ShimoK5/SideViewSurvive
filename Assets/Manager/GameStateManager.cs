using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE { 
StartFade,          //開始フェード
StartCameraMotion,  //スタートカメラ移動
StartPlayerMotion,  //プレイヤースタート演出  
Game,               //ゲーム
Pause,              //ポーズ
DeadPlayer,         //プレイヤー死亡時演出
EndPlayerMotion,    //プレイヤーゴール演出
EndFade,            //終了フェード
}


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GAME_STATE GameState;        //インスペクターから初期値を選択

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
