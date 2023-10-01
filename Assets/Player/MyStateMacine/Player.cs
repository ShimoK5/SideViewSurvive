using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATE
{
    NONE,
    STAND,
    AIR,
    RUN,
    GYM_CLOTHES,    //体操着
    RECORDER,       //リコーダー
    ERASER,         //消しゴム
}

public class Player : MonoBehaviour
{
    static public Player instance;
    PlayerIF m_Player;

    public PLAYER_STATE Temp;

    PLAYER_STATE OuterNextState;//外部から編集されたNextState

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OuterNextState = PLAYER_STATE.NONE;

        m_Player = new PlayerIF();
        m_Player.CustumStart();

        m_Player = new PlayerAir(m_Player);
    }

    // Update is called once per frame
    void Update()
    {
        Temp = m_Player.PlayerState;
        m_Player.CustumUpdate();
    }

    void FixedUpdate()
    {
        CheckState();
        m_Player.CustumFixed();
    }

    void CheckState()
    {
        //外部からの変更信号があればそれに従う
        if(OuterNextState != PLAYER_STATE.NONE)
        {
            m_Player.NextPlayerState = OuterNextState;
            OuterNextState = PLAYER_STATE.NONE;
        }

        if(m_Player.PlayerState != m_Player.NextPlayerState)
        {
            ChengeState(m_Player.NextPlayerState);

            m_Player.PlayerState = m_Player.NextPlayerState;
        }
    }

    void ChengeState(PLAYER_STATE playerState)
    {
        switch (playerState)
        {
            case PLAYER_STATE.STAND:
                m_Player = new PlayerStand(m_Player);
                break;

            case PLAYER_STATE.RUN:
                m_Player = new PlayerRun(m_Player);
                break;

            case PLAYER_STATE.AIR:
                m_Player = new PlayerAir(m_Player);
                break;

            case PLAYER_STATE.GYM_CLOTHES:
                m_Player = new PlayerGymClothes(m_Player);
                break;

            case PLAYER_STATE.RECORDER:
                m_Player = new PlayerRecorder(m_Player);
                break;

            case PLAYER_STATE.ERASER:
                m_Player = new PlayerEraser(m_Player);
                break;

            case PLAYER_STATE.NONE:
                //m_Player = new PlayerNone(m_Player);
                break;
        }
    }

    //外部からステート変更
    public void SetOuterState(PLAYER_STATE ons, float otherVelX = 0, float otherVelY = 0, 
         bool resetSelfVelX = false, bool resetSelfVelY = false,
         bool resetOtherVelX = false, bool resetOtherVelY = false)
    {
        OuterNextState = ons;

        if (resetSelfVelX)
        {
            m_Player.SelfVel.x = 0;
        }
        if (resetSelfVelY)
        {
            m_Player.SelfVel.y = 0;
        }
        if (resetOtherVelX)
        {
            m_Player.OtherVel.x = 0;
        }
        if (resetOtherVelY)
        {
            m_Player.OtherVel.y = 0;
        }

        m_Player.OtherVel += new Vector2(otherVelX, otherVelY);
    }

    //外部からvelocity変更
    public void SetOuterVel(float otherVelX = 0, float otherVelY = 0,
        bool resetSelfVelX = false, bool resetSelfVelY = false,
        bool resetOtherVelX = false, bool resetOtherVelY = false)
    {
       
        if (resetSelfVelX)
        {
            m_Player.SelfVel.x = 0;
        }
        if (resetSelfVelY)
        {
            m_Player.SelfVel.y = 0;
        }
        if (resetOtherVelX)
        {
            m_Player.OtherVel.x = 0;
        }
        if (resetOtherVelY)
        {
            m_Player.OtherVel.y = 0;
        }

        m_Player.OtherVel += new Vector2(otherVelX, otherVelY);
    }

    //外部から直値座標変更(移動床)
    public void AddPosition(Vector3 AddVolume)
    {
        if (m_Player.isGround)
            transform.position += AddVolume;
    }

    public bool GetisGround()
    {
        return m_Player.isGround;
    }

    public PlayerIF GetM_Player()
    {
        return m_Player;
    }
}
