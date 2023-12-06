using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerCanvas : MonoBehaviour
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
        
    }

    void FixedUpdate()
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
                if (!Player.instance.inScreen && GameStateManager.instance.GameState == GAME_STATE.Game)
                {
                    m_Canvas.enabled = true;
                }
                else
                {
                    m_Canvas.enabled = false;
                }
                break;

        }

       


        Vector3 Position = Vector3.zero;
        Position.x = CameraPos2.instance.transform.position.x - CameraPos2.instance.ViewWidth * 0.5f 
            + GetComponent<RectTransform>().sizeDelta.x * 0.5f;
        Position.y = Player.instance.transform.position.y;
        Position.z = Player.instance.transform.rotation.z;

        GetComponent<RectTransform>().position = Position;
        //transform.position = Player.instance.transform.position;
    }
}
