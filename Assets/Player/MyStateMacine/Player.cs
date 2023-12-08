using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine;
using Spine.Unity;
public enum PLAYER_STATE
{
    NONE,
    STAND,
    AIR,
    RUN,

    TSUNAGI,

    UMBRELLA,    //体操着
    RECORDER,       //リコーダー
    ERASER,         //消しゴム
    SACRIFICE,      //身代わり
    AIR_CANNON,     //空気砲
    BAG,            //ランドセル
    RULER,          //定規
    WHISTLE,        //笛

    DAMAGE,         //被弾

    DEAD,           //死亡
    GOAL,           //ゴール
}

public class Player : MonoBehaviour
{
    static public Player instance;  //静的な実態
    PlayerIF m_Player;              //ステートマシーンの実態

    public PLAYER_STATE TempState;       //ReadOnly

    PLAYER_STATE OuterNextState;//外部から編集されたNextState
    public int HitPoint;        //HP
    //[Header("ダメージのクールタイム")]
    [SerializeField] int InvisibleCoolTime;  //ダメージクールタイム（無敵時間）
    bool DamageInvincible = false;            //無敵かどうか
    int InvincibleFlameCount = 0;           //ダメージ時加算カウント
    int ActionInvisibleAnimCount = 0;       //無敵アニメーション用のカウント
    float GoalPosX = 0;                       //ゴールのX座標
    PlayerAnimSpine PlayerAnim;                 //プレイヤーのアニメーション
    MeshRenderer PlayerMeshRenderer;            //プレイヤーのレンダラー

    public bool inScreen = true;                   //スクリーン内かどうか
    public int ScreenOutCnt = 0;
    [Header("スクリーンを何フレーム出たら死ぬか")]
    [SerializeField] public int MAX_SCREEN_OUT_CNT;  //ダメージクールタイム（無敵時間）

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnim = GameObject.Find("PlayerAnim").GetComponent<PlayerAnimSpine>();
        PlayerMeshRenderer = GameObject.Find("PlayerAnim").GetComponent<MeshRenderer>();
        PlayerMeshRenderer.enabled = false;

        OuterNextState = PLAYER_STATE.NONE;

        m_Player = new PlayerIF();
        m_Player.CustumStart();

        m_Player = new PlayerStand(m_Player);

        GoalPosX = GameObject.Find("GoalObj").transform.position.x;
        //HitPoint = 200;
    }

    // Update is called once per frame
    void Update()
    {
        TempState = m_Player.PlayerState;
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                UpdateGame();
                break;
            default:
                break;
        }
    }

    void UpdateGame()
    {
       
        m_Player.CustumUpdate();
    }


    void FixedUpdate()
    {
        //プレイヤーが通常色になる処理
        PlayerAnim.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.0f);

        //ゴール演出
        if (transform.position.x >= GoalPosX && GameStateManager.instance.GameState == GAME_STATE.Game)
        {
            transform.position = new Vector3(GoalPosX, transform.position.y, transform.position.z);
            //プレイヤーが通常色になる処理
            PlayerAnim.Anim.skeleton.SetColor(new Color(1f, 1f, 1f, 1f));
            //ステート変更
            GameStateManager.instance.GameState = GAME_STATE.EndPlayerMotion;
            
        }
        //プレイヤー死亡演出
        else if (HitPoint <= 0 && GameStateManager.instance.GameState == GAME_STATE.Game)
        {
            //プレイヤーが通常色になる処理
            PlayerAnim.Anim.skeleton.SetColor(new Color(1f, 1f, 1f, 1f));
            //ステート変更
            GameStateManager.instance.GameState = GAME_STATE.DeadPlayerStop;
            //エフェクト非表示
            m_Player.NowAirBorne = false;
            //カメラ揺れ
            CameraPos2.instance.HitSwing(new Vector3(0, -1, 0));

        }

        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                PlayerMeshRenderer.enabled = true;
                FixedGame();
                break;

            case GAME_STATE.DeadPlayer:
                SetOuterState(PLAYER_STATE.DEAD);
                CheckState();
                m_Player.CustumFixed();
                break;

            case GAME_STATE.EndPlayerMotion:
                SetOuterState(PLAYER_STATE.GOAL);
                CheckState();
                m_Player.CustumFixed();
                break;

            case GAME_STATE.StartPlayerMotion:
            
                //m_Player.CustumFixed();
                break;
            default:
                break;
        }

        //スクリーン内判定
        Check_inScreen();
    }

    void FixedGame()
    {
        DamageCheck();
        CheckState();
        m_Player.CustumFixed();
    }

    void Check_inScreen()
    {
        float CameraMinX = CameraPos2.instance.transform.position.x - CameraPos2.instance.ViewWidth * 0.5f;
        float CameraMaxX = CameraPos2.instance.transform.position.x + CameraPos2.instance.ViewWidth * 0.5f;
        float EnemyMinX = transform.position.x - m_Player.Size.x * 0.5f;
        float EnemyMaxX = transform.position.x + m_Player.Size.x * 0.5f;

        if (EnemyMinX <= CameraMaxX && EnemyMaxX >= CameraMinX)
        {
            inScreen = true;
        }
        else
        {
            inScreen = false;
        }

       
        if(GameStateManager.instance.GameState == GAME_STATE.Game)
        {
            //スクリーン外ならカウントアップする
            if (inScreen == false )
            {
                ScreenOutCnt = Mathf.Min(ScreenOutCnt + 1, MAX_SCREEN_OUT_CNT);
            }
            //スクリーン内ならカウントダウンする
            else
            {
                int CountDownNum = 3;
                ScreenOutCnt = Mathf.Max(ScreenOutCnt - CountDownNum, 0);
            }

            

        }
        //一定時間スクリーンから出たら死亡
        if (ScreenOutCnt >= MAX_SCREEN_OUT_CNT)
        {
            HitPoint = 0;
        }

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

            case PLAYER_STATE.TSUNAGI:
                m_Player = new PlayerTsunagi(m_Player);
                break;

            case PLAYER_STATE.UMBRELLA:
                m_Player = new PlayerUmbrella(m_Player);
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

            case PLAYER_STATE.AIR_CANNON:
                m_Player = new PlayerAirCannon(m_Player);
                break; 
            
            case PLAYER_STATE.BAG:
                m_Player = new PlayerBag(m_Player);
                break;
            
            case PLAYER_STATE.RULER:
                m_Player = new PlayerRuler(m_Player);
                break;
            
            case PLAYER_STATE.WHISTLE:
                m_Player = new PlayerWhistle(m_Player);
                break;

            case PLAYER_STATE.DAMAGE:
                m_Player = new PlayerDamage(m_Player);
                break;

            case PLAYER_STATE.DEAD:
                m_Player = new PlayerDead(m_Player);
                break;

            case PLAYER_STATE.GOAL:
                m_Player = new PlayerGoal(m_Player);
                break;

            case PLAYER_STATE.NONE:
                m_Player = new PlayerTsunagi(m_Player);
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

    public PlayerAnimSpine GetAnim()
    {
        return PlayerAnim;
    }

    public void HitEnemy(Vector2 EtoP_Vel)
    {
        //無敵ならしない処理
        if (DamageInvincible)
            return;
        if (m_Player.ActionInvisible)
            return;


        {
#if false
        //プレイヤーVel編集
        SetOuterVel(EtoP_Vel.x * Player.instance.GetM_Player().KNOCK_BACK_POWER
            , EtoP_Vel.y * Player.instance.GetM_Player().KNOCK_BACK_POWER, true, true, true, true);
#else
            //無敵じゃなければ

            //無敵になる
            InvisibleOn(120);
            //プレイヤーVel編集 & 向き変更
            if (EtoP_Vel.x >= 0)
            {
                SetOuterVel(GetM_Player().KNOCK_BACK_POWER * 1.5f
                , GetM_Player().KNOCK_BACK_POWER * 0.2f, true, true, true, true);
                
                transform.localEulerAngles = new Vector3(0, -90, 0);
            }
            else
            {
                SetOuterVel(-GetM_Player().KNOCK_BACK_POWER * 1.5f
                , GetM_Player().KNOCK_BACK_POWER * 0.2f, true, true, true, true);
               
                transform.localEulerAngles = new Vector3(0, 90, 0);
            }

            //プレイヤーステート変更
            PlayerStateIsDamage();
#endif
            HitPoint = Mathf.Max(0, HitPoint - 1);

            //被ダメ演出呼び出し
            //HPまだあれば
            if(HitPoint > 0)
            {
                //カメラ揺れ
                CameraPos2.instance.HitSwing(new Vector3(EtoP_Vel.x, EtoP_Vel.y, 0));
                //キャンバスプレハブ生成
                //GameObject Canvas = (GameObject)Resources.Load("DamageEffectCanvas");
                //Canvas = Instantiate(Canvas, Vector3.zero, Quaternion.Euler(Vector3.zero));
            }
            //HP０
            else
            {

            }
            
        }

    }

    void PlayerStateIsDamage()
    {
        //上書き可能なセルフステートを変更
        m_Player.NextPlayerState = PLAYER_STATE.DAMAGE;
    }

    void DamageCheck()
    {
        if (DamageInvincible)
        {
            //フレームカウント加算
            InvincibleFlameCount++;

            if (InvincibleFlameCount > InvisibleCoolTime)
            {
                InvincibleFlameCount = 0;
                DamageInvincible = false;
            }

        }

        if (DamageInvincible)
        {
#if false
            //プレイヤーが赤くなる処理
            PlayerAnim.Anim.skeleton.SetColor(new Color(1f, 0.6f, 0.6f));
#else
            //プレイヤーが点滅する処理
            if(InvincibleFlameCount / 8 % 2 == 0)
            {
                PlayerAnim.Anim.skeleton.SetColor(new Color(1, 1, 1 , 0.9f));
            }
            else
            {
                PlayerAnim.Anim.skeleton.SetColor(new Color(1, 1, 1, 0.6f));
            }
#endif

        }
        else if (m_Player.ActionInvisible)
        {
            ActionInvisibleAnimCount++;


            if (ActionInvisibleAnimCount / 8 % 2 == 0)
            {
                //プレイヤーが白くなる処理
                PlayerAnim.GetComponent<Renderer>().material. SetFloat("_FillPhase", 0.5f);
            }
            else
            {
                //プレイヤーが通常色になる処理
                PlayerAnim.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.0f);
            }

            //あふれ防止
            if(ActionInvisibleAnimCount >= 16)
            {
                ActionInvisibleAnimCount = 0;
            }
            
        }
        else
        {
            //アニメ用カウントリセット
            ActionInvisibleAnimCount = 0;

            //プレイヤーが通常色になる処理
            PlayerAnim.Anim.skeleton.SetColor(new Color(1f, 1f, 1f,1f));
        }
    }

    void InvisibleOn(int damageCoolTime)
    {
        DamageInvincible = true;
        InvisibleCoolTime = damageCoolTime;
    }

    IEnumerator SceneChange()
    {
        //0.5秒停止
        yield return new WaitForSeconds(0.5f);

        //シーンリロード
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
