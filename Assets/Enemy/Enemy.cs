using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ENEMY_STATE
{
    NONE,
    WALK,
    FLY,
    DEAD
}

public class Enemy : MonoBehaviour
{
    EnemyIF m_Enemy;

    public ENEMY_STATE Temp;

    [Header("エネミーのタイプ")]
    [SerializeField] ENEMY_STATE InitEnmyType;

    ENEMY_STATE OuterNextState;//外部から編集されたNextState

    void Awake()
    {
        //instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OuterNextState = ENEMY_STATE.NONE;

        m_Enemy = new EnemyIF();
        m_Enemy.CustumStart(GetComponent<Transform>());

        switch (InitEnmyType)
        {
            case ENEMY_STATE.WALK:
                m_Enemy = new EnemyWalk(m_Enemy);
                break;

            case ENEMY_STATE.FLY:
                m_Enemy = new EnemyFly(m_Enemy);
                break;

            default:
                m_Enemy = new EnemyWalk(m_Enemy);
                break;

        }


        
    }

    // Update is called once per frame
    void Update()
    {
        Temp = m_Enemy.EnemyState;
        m_Enemy.CustumUpdate();
    }

    void FixedUpdate()
    {
        CheckState();
        m_Enemy.CustumFixed();
    }

    void CheckState()
    {
        //外部からの変更信号があればそれに従う
        if (OuterNextState != ENEMY_STATE.NONE)
        {
            m_Enemy.NextEnemyState = OuterNextState;
            OuterNextState = ENEMY_STATE.NONE;
        }

        if (m_Enemy.EnemyState != m_Enemy.NextEnemyState)
        {
            ChengeState(m_Enemy.NextEnemyState);

            m_Enemy.EnemyState = m_Enemy.NextEnemyState;
        }
    }

    void ChengeState(ENEMY_STATE playerState)
    {
        switch (playerState)
        {
            case ENEMY_STATE.WALK:
                m_Enemy = new EnemyWalk(m_Enemy);
                break;

            case ENEMY_STATE.FLY:
                m_Enemy = new EnemyFly(m_Enemy);
                break;

            case ENEMY_STATE.DEAD:
                m_Enemy = new EnemyDead(m_Enemy);
                break;

            default:
                break;

        }
    }

    //外部からステート変更
    public void SetOuterState(ENEMY_STATE ons, float otherVelX = 0, float otherVelY = 0,
         bool resetSelfVelX = false, bool resetSelfVelY = false,
         bool resetOtherVelX = false, bool resetOtherVelY = false)
    {
        OuterNextState = ons;

        if (resetSelfVelX)
        {
            m_Enemy.SelfVel.x = 0;
        }
        if (resetSelfVelY)
        {
            m_Enemy.SelfVel.y = 0;
        }
        if (resetOtherVelX)
        {
            m_Enemy.OtherVel.x = 0;
        }
        if (resetOtherVelY)
        {
            m_Enemy.OtherVel.y = 0;
        }

        m_Enemy.OtherVel += new Vector2(otherVelX, otherVelY);
    }

    //外部からvelocity変更
    public void SetOuterVel(float otherVelX = 0, float otherVelY = 0,
        bool resetSelfVelX = false, bool resetSelfVelY = false,
        bool resetOtherVelX = false, bool resetOtherVelY = false)
    {

        if (resetSelfVelX)
        {
            m_Enemy.SelfVel.x = 0;
        }
        if (resetSelfVelY)
        {
            m_Enemy.SelfVel.y = 0;
        }
        if (resetOtherVelX)
        {
            m_Enemy.OtherVel.x = 0;
        }
        if (resetOtherVelY)
        {
            m_Enemy.OtherVel.y = 0;
        }

        m_Enemy.OtherVel += new Vector2(otherVelX, otherVelY);
    }

    //外部から直値座標変更(移動床)
    public void AddPosition(Vector3 AddVolume)
    {
        if (m_Enemy.isGround)
            transform.position += AddVolume;
    }

    public bool GetisGround()
    {
        return m_Enemy.isGround;
    }

    public EnemyIF GetM_Enemy()
    {
        return m_Enemy;
    }
}
