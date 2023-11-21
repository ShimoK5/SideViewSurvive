using UnityEngine;

public class OtonaBlock : Block
{
    public enum BLOCK_TYPE {
    LEFT,
    RIGHT,
    }

    public BLOCK_TYPE BlockType;


    void Start()
    {
        Size = transform.GetComponent<MeshRenderer>().GetComponent<MeshRenderer>().bounds.size;

        if (CameraPos2.instance.GetComponent<CameraPos2>().enabled)
        {
            float PosX = 0;
            switch (BlockType)
            {
                case BLOCK_TYPE.LEFT:
                    PosX = CameraPos2.instance.DefaultPos.x - CameraPos2.instance.ViewWidth * 0.5f - Size.x * 0.5f;
                    break;
                case BLOCK_TYPE.RIGHT:
                    PosX = CameraPos2.instance.DefaultPos.x + CameraPos2.instance.ViewWidth * 0.5f + Size.x * 0.5f;
                    break;
            }
            transform.position = new Vector3(PosX, transform.position.y, transform.position.z);
        }

        
    }

    void FixedUpdate()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                OldPos = transform.position;

                if (CameraPos2.instance.GetComponent<CameraPos2>().enabled)
                {
                    float PosX = 0;
                    switch (BlockType)
                    {
                        case BLOCK_TYPE.LEFT:
                            PosX = CameraPos2.instance.DefaultPos.x - CameraPos2.instance.ViewWidth * 0.5f - Size.x * 0.5f;
                            break;
                        case BLOCK_TYPE.RIGHT:
                            PosX = CameraPos2.instance.DefaultPos.x + CameraPos2.instance.ViewWidth * 0.5f + Size.x * 0.5f;
                            break;
                    }
                    transform.position = new Vector3(PosX, transform.position.y, transform.position.z);
                }

                break;
        }
    }
}
