using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector3 Size;
    public Vector3 OldPos;
    void Start()
    {
        Size = transform.GetComponent<MeshRenderer>().GetComponent<MeshRenderer>().bounds.size;
    }

    void FixedUpdate()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                OldPos = transform.position;
                break;
        }
    }
}
