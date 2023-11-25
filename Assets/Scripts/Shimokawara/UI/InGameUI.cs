using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    Canvas m_Canvas;

    // Start is called before the first frame update
    void Start()
    {
        m_Canvas = GetComponent<Canvas>();

        m_Canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.StartFade:
            case GAME_STATE.StartCameraMotion:

            case GAME_STATE.DeadPlayerStop:
            case GAME_STATE.DeadPlayer:
            case GAME_STATE.EndPlayerMotion:
            case GAME_STATE.EndFade:
                m_Canvas.enabled = false;
                break;

            case GAME_STATE.StartPlayerMotion:
            case GAME_STATE.Game:
            case GAME_STATE.Pause:
                m_Canvas.enabled = true;
                break;

        }

    }
}
