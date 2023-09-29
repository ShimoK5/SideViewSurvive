using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public enum RhythmAction {
        GymClothes,     //体操着
        Recorder,       //リコーダー
        Eraser,         //消しゴム

        None            //なし
    }

    static int BeatNum = 8; //拍数
    [Header("アクション配列")]
    [SerializeField] RhythmAction[] ActionArray = new RhythmAction[BeatNum];
    [Header("リズム間隔（フレーム数）")]
    [SerializeField] int BeatTempo;
    int FCnt = 0;//フレームカウント

    // Start is called before the first frame update
    void Start()
    {
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
        if (Player.instance.GetisGround())
        {
            Player.instance.SetOuterState(PLAYER_STATE.RUN);
        }
        else
        {
            Player.instance.SetOuterState(PLAYER_STATE.AIR);
        }
    }

}
