using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour
{
    public enum RhythmAction
    {
        Umbrella,     //体操着
        Recorder,       //リコーダー
        Eraser,         //消しゴム
        Sacrifice,      //身代わり
        AirCannon,      //空気砲
        Bag,            //ランドセル
        Ruler,          //定規
        Whistle,        //笛
        None            //なし
    }
    public static RhythmManager Instance;
    static int BeatNum = 8; //拍数
    [Header("アクション配列")]
    [SerializeField] public RhythmAction[] ActionArray = new RhythmAction[BeatNum];
    [Header("リズム間隔（フレーム数）")]
    [SerializeField] public int BeatTempo;
    public int FCnt = 0;//フレームカウント

    string[] BGMName = { "InGame1", "InGame2", "InGame3", "InGame4" } ;
    int PlayBGMCnt = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        //ActionArray = new RhythmAction[BeatNum];
    }

    void Start()
    {
        FCnt = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                FixedGame();
                break;

            //case GAME_STATE.DeadPlayer:
            //    FCnt = 0;
            //    break;
            default:
                if(SceneManager.GetActiveScene().name == "SetScene")
                {
                    FixSet();
                }
                break;
        }
    }


    void ChangeStateDefault()
    {
        Player.instance.SetOuterState(PLAYER_STATE.TSUNAGI);

#if false
        if (Player.instance.GetisGround())
        {
            if (Mathf.Abs(Player.instance.GetM_Player().SelfVel.x/* + OtherVel.x*/) < Player.instance.GetM_Player().GetStandSpeed() )
            {
                Player.instance.SetOuterState(PLAYER_STATE.STAND);
            }
            else
            {
                Player.instance.SetOuterState(PLAYER_STATE.RUN);
            }
            
        }
        else
        {
            Player.instance.SetOuterState(PLAYER_STATE.AIR);
        }
    
#endif

    }


    void FixedGame()
    {
        //週の初めに
        if(FCnt == 0)
        {
            //2週に一回 
            if (PlayBGMCnt % 1 == 0)
            {
                NewSoundManager.instance.StopBGM();
                NewSoundManager.instance.PlayBGM(BGMName[PlayBGMCnt / 1]);
            }
            //カウント加算
            PlayBGMCnt++;
            //あふれ防止
            if (PlayBGMCnt >= 4 * 1)
            {
                PlayBGMCnt = 0;
            }

        }


        //フレームカウント加算
        FCnt++;

        //周期の直前にステートリセット
        if (FCnt % BeatTempo == BeatTempo - 1)
        {
            ChangeStateDefault();
        }

        //周期が来たら
        if (FCnt % BeatTempo == 0)
        {
            //アクションを指示する
            int ActionIndex = FCnt / BeatTempo - 1;
            switch (ActionArray[ActionIndex])
            {
                case RhythmAction.Umbrella:
                    Player.instance.SetOuterState(PLAYER_STATE.UMBRELLA);
                    break;

                case RhythmAction.Recorder:
                    Player.instance.SetOuterState(PLAYER_STATE.RECORDER);
                    break;

                case RhythmAction.Eraser:
                    Player.instance.SetOuterState(PLAYER_STATE.ERASER);
                    break;

                case RhythmAction.Sacrifice:
                    Player.instance.SetOuterState(PLAYER_STATE.SACRIFICE);
                    break;

                case RhythmAction.AirCannon:
                    Player.instance.SetOuterState(PLAYER_STATE.AIR_CANNON);
                    break;

                case RhythmAction.Bag:
                    Player.instance.SetOuterState(PLAYER_STATE.BAG);
                    break;

                case RhythmAction.Ruler:
                    Player.instance.SetOuterState(PLAYER_STATE.RULER);
                    break;

                case RhythmAction.Whistle:
                    Player.instance.SetOuterState(PLAYER_STATE.WHISTLE);
                    break;

                case RhythmAction.None:
                    ChangeStateDefault();


                    break;

                default:
                    break;
            }

        }

        //最終拍でフレームリセット
        if (FCnt >= BeatNum * BeatTempo)
        {
            FCnt = 0;
        }
    }

    void FixSet()
    {
        //フレームカウント加算
        FCnt++;

        //周期の直前にステートリセット
        //if (FCnt % BeatTempo == BeatTempo - 1)
        //{
        //    ChangeStateDefault();
        //}


        //最終拍でフレームリセット
        if (FCnt >= BeatNum * BeatTempo)
        {
            FCnt = 0;
        }
    }
}
