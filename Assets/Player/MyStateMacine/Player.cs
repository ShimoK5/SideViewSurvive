using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum PLAYER_STATE
{
    NONE,
    STAND,
    AIR,
    RUN,
    GYM_CLOTHES,    //体操着
    RECORDER,       //リコーダー
    ERASER,         //消しゴム
    SACRIFICE,      //身代わり
}

public class Player : MonoBehaviour
{
    static public Player instance;  //静的な実態
    PlayerIF m_Player;              //ステートマシーンの実態

    public PLAYER_STATE Temp;       //ReadOnly

    PLAYER_STATE OuterNextState;//外部から編集されたNextState
    public int HitPoint;        //HP
    [Header("ダメージのクールタイム")]
    [SerializeField] int DAMAGE_COOL_TIME;  //ダメージクールタイム（無敵時間）
    bool Invincible = false;            //無敵かどうか
    int DamageFlameCount = 0;           //ダメージ時加算カウント
    GameObject AnimObj;

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
        HitPoint = 3;
        AnimObj = GameObject.Find("Anim");
    }

    // Update is called once per frame
    void Update()
    {
        Temp = m_Player.PlayerState;
        m_Player.CustumUpdate();

        //シーンリロード処理
        if(HitPoint <= 0)
        {
            StartCoroutine("SceneChange");
        }
    }

    void FixedUpdate()
    {
        DamageCheck();
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

            case PLAYER_STATE.SACRIFICE:
                m_Player = new PlayerSacrifice(m_Player);
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
    public void AddPosition(Vector3 addVolume)
    {
        if (m_Player.isGround)
            transform.position += addVolume;
    }

    public bool GetisGround()
    {
        return m_Player.isGround;
    }

    public PlayerIF GetM_Player()
    {
        return m_Player;
    }

    public void HitEnemy(Vector2 EtoP_Vel)
    {
        if (!Invincible)
        {
#if false
        //プレイヤーVel編集
        SetOuterVel(EtoP_Vel.x * Player.instance.GetM_Player().KNOCK_BACK_POWER
            , EtoP_Vel.y * Player.instance.GetM_Player().KNOCK_BACK_POWER, true, true, true, true);
#else
            //無敵じゃなければ

            //無敵になる
            Invincible = true;
            //プレイヤーVel編集
            if (EtoP_Vel.x >= 0)
            {
                SetOuterVel(GetM_Player().KNOCK_BACK_POWER * 3.5f
                , GetM_Player().KNOCK_BACK_POWER * 0.2f, true, true, true, true);
            }
            else
            {
                SetOuterVel(-GetM_Player().KNOCK_BACK_POWER * 3.5f
                , GetM_Player().KNOCK_BACK_POWER * 0.2f, true, true, true, true);
            }
        

#endif
            HitPoint = Mathf.Max(0, HitPoint - 1);
        }

    }

    void DamageCheck()
    {
        if (Invincible)
        {
            //フレームカウント加算
            DamageFlameCount++;

            if (DamageFlameCount > DAMAGE_COOL_TIME)
            {
                DamageFlameCount = 0;
                Invincible = false;
            }

        }

        if (Invincible)
        {
            AnimObj.GetComponent<Renderer>().material.color = new Color(1, 0.3f, 0.3f, 1);
        }
        else
        {
            AnimObj.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator SceneChange()
    {
        //0.5秒停止
        yield return new WaitForSeconds(0.5f);

        //シーンリロード
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
