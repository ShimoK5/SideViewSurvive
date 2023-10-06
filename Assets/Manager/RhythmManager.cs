using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public enum RhythmAction {
        GymClothes,     //体操着
        Recorder,       //リコーダー
        Eraser,         //消しゴム
        Sacrifice,      //身代わり
        None            //なし
    }
    public static RhythmManager Instance;
    static int BeatNum = 8; //拍数
    [Header("アクション配列")]
    [SerializeField] RhythmAction[] ActionArray = new RhythmAction[BeatNum];
    [Header("リズム間隔（フレーム数）")]
    [SerializeField] public int BeatTempo;
    public int FCnt = 0;//フレームカウント

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //ActionArray = new RhythmAction[BeatNum];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                case RhythmAction.GymClothes:
                    Player.instance.SetOuterState(PLAYER_STATE.GYM_CLOTHES);
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


    void ChangeStateDefault()
    {
        //Player.instance.SetOuterState(PLAYER_STATE.NONE);

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
    }

}
