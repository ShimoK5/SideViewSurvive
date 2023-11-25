using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();

        m_Camera.enabled = false;
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
                m_Camera.enabled = false;
                break;

            case GAME_STATE.StartPlayerMotion:
            case GAME_STATE.Game:
            case GAME_STATE.Pause:
                m_Camera.enabled = true;
                break;

        }

    }
}
