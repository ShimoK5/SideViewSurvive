using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ENEMY_STATE
{
    NONE,
    WALK,
    FLY,
    ZIGZAG,
    TRACKING,
    CIRCLE,
    DEAD
}

public class Enemy : MonoBehaviour
{
    EnemyIF m_Enemy;
    bool IsOnes = false;

    public ENEMY_STATE Temp;
    public bool Operation = false;

    [Header("設置型")]
    public bool stationary = false;

    [Header("エネミーのタイプ")]
    [SerializeField] ENEMY_STATE InitEnmyType;
    //[Header("円運動用の半径")]
    //public float Radius = 0.0f;

    ENEMY_STATE OuterNextState;//外部から編集されたNextState

    void Awake()
    {
        //instance = this;
    }

    void OnEnable()
    {
        Start();
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
            
            case ENEMY_STATE.ZIGZAG:
                m_Enemy = new EnemyZigzag(m_Enemy);
                break;
            
            case ENEMY_STATE.TRACKING:
                m_Enemy = new EnemyTracking(m_Enemy);
                break;
            
            case ENEMY_STATE.CIRCLE:
                m_Enemy = new EnemyCircularMotion(m_Enemy);
                break;

            default:
                m_Enemy = new EnemyWalk(m_Enemy);
                break;

        }


        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                UpdateGame();
                break;
        }
        
    }

    void UpdateGame()
    {
        Temp = m_Enemy.EnemyState;
        m_Enemy.CustumUpdate();
    }

    void FixedUpdate()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                FixedGame();
                break;
        }
    }

    void FixedGame()
    {
        CheckState();
        if ((stationary != !Operation) || !IsOnes)
        {
            m_Enemy.CustumFixed();
            IsOnes = true;
        }
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

            case ENEMY_STATE.ZIGZAG:
                m_Enemy = new EnemyZigzag(m_Enemy);
                break;
            
            case ENEMY_STATE.TRACKING:
                m_Enemy = new EnemyTracking(m_Enemy);
                break;

            case ENEMY_STATE.CIRCLE:
                m_Enemy = new EnemyCircularMotion(m_Enemy);
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

    public void SetEnemyDeadData(Vector2 vel, bool deadCntFlag = true)
    {
        m_Enemy.DeadVector = vel.normalized;
        m_Enemy.DeadCntFlag = deadCntFlag;
        if(deadCntFlag)
        {
            //ここにカメラ範囲内のif分を噛ませる
            CameraPos.instance.HitSwing(new Vector3(m_Enemy.DeadVector.x, m_Enemy.DeadVector.y, 0));
            SoundManager.instance.SEPlay("仮SE");
        }
    }

    public ENEMY_STATE GetEnemyState()
    {
        return InitEnmyType;
    }

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーとの衝突
        if (other.GetComponent<Player>())
        {
            //Playerからエネミーへのベクトル
            Vector3 PtoE_Vel = (transform.position - other.transform.position).normalized;
            //プレイヤーVel編集
            other.GetComponent<Player>().HitEnemy(-PtoE_Vel);
            //EnemyDead化　＆　DeadVector編集
            SetOuterState(ENEMY_STATE.DEAD);
            SetEnemyDeadData(PtoE_Vel.normalized,false);

        }
    }
}
