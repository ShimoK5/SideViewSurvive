using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE { 
StartFade,
StartCameraMotion,
StartPlayerMotion,
Game,
Pause,
EndPlayerMotion,
EndFade,
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
