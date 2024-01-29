using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputRhythm : MonoBehaviour
{
    public static  InputRhythm instance = null;                                                         //インスタンス保管庫
    [SerializeField] RhythmManager.RhythmAction[] Action = new RhythmManager.RhythmAction[8];           //インスペクター内でいじれるよう
    [SerializeField] private bool OneAction;                                                            //各シーンの変化毎に一回分のアクションを起こす用の変数
    private bool FistGameChange = false;                                                                //Gameシーンでのノート変化用
    private bool FirstSet = false;
    [SerializeField]string GameSceneName = null;                                                        //Gameシーンの名前保管用

    void Awake()
    {
        //シングルトン化
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            OneAction = false;
        }

        for (int Number = 0; Number < 8; Number++)
        {
            Action[Number] = RhythmManager.RhythmAction.None;
        }
        FirstSet = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameSceneName = "Game";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //OneActionをステートから取得
        OneAction = StateInputRhythm.instance.GetOneAction();

        //各シーンに変更した際の一回行動分
        if(OneAction == true)
        {
            //ここでOneActionを変更しています。
            StateInputRhythm.STATE State = StateInputRhythm.instance.GetState();
            switch (State)
            {
                case StateInputRhythm.STATE.FirstSet:
                    SetRhythmManager();
                    ChangeNoteBox();
                    break;

                case StateInputRhythm.STATE.BreakSet:
                    SetRhythmManager();
                    ChangeNoteBox();
                    break;

                case StateInputRhythm.STATE.TitleSet:
                    ResetMetronome();
                    FirstSet = false;
                    break;

                case StateInputRhythm.STATE.Movie_01Set:
                    break;

                case StateInputRhythm.STATE.Movie_02Set:
                    if (FirstSet == false)
                    {
                        FirstSet = true;
                    }
                    GameObject MovieChange = GameObject.Find("MovieEndSceneChange");
                    if (MovieChange != null)
                    {
                        MovieChange.GetComponent<MovieEndSceneChange>().GetSceneName(GameSceneName);
                        MovieChange = GameObject.Find("GaugeImage");
                        MovieChange.GetComponent<CircleGauge>().SetSceneName(GameSceneName);
                    }
                    break;

                case StateInputRhythm.STATE.NowGameSet:
                    SetRhythmManager();
                    FistGameChange = true;
                    
                    break;

                case StateInputRhythm.STATE.ResetSet:
                    break;

                default:
                    break;
            }
        }

        if (FistGameChange == true)
        {
            if (GameStateManager.instance.GameState == GAME_STATE.StartFade)
            {
                if (FistGameChange == true)
                {
                    StrageNoteBox Storage = GameObject.Find("NoteBox").GetComponent<StrageNoteBox>();
                    Storage.Change();
                    FistGameChange = false;
                }
            }
        }
    }

  

    void SetRhythmManager()
    {
        for (int Number = 0; Number < 8; Number++)
        {
            switch (Action[Number])
            {
                case RhythmManager.RhythmAction.Umbrella:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.Umbrella;
                    break;

                case RhythmManager.RhythmAction.Recorder:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.Recorder;
                    break;

                case RhythmManager.RhythmAction.Eraser:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.Eraser;
                    break;

                case RhythmManager.RhythmAction.Sacrifice:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.Sacrifice;
                    break;

                case RhythmManager.RhythmAction.AirCannon:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.AirCannon;
                    break;

                case RhythmManager.RhythmAction.Bag:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.Bag;
                    break;

                case RhythmManager.RhythmAction.Ruler:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.Ruler;
                    break;

                case RhythmManager.RhythmAction.Whistle:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.Whistle;
                    break;

                case RhythmManager.RhythmAction.None:
                    RhythmManager.Instance.ActionArray[Number] = RhythmManager.RhythmAction.None;
                    break;

                default:
                    break;
            }
        }
    }
    public void SetSceneName(string Name)
    {
        GameSceneName = Name;
    }

    public string GetSceneName()
    {
        return GameSceneName;
    }

    public void ResetMetronome()
    {
        for (int i = 0; i < 8; i++)
        {
            Action[i] = RhythmManager.RhythmAction.None;
        }
    }

    public void SetMetronome(int Number,RhythmManager.RhythmAction ArrayAction)
    {
        //他のアクションが存在し、且つアクションが無い状態に変更するとき
        if (Action[Number] != RhythmManager.RhythmAction.None)
        {
            ActionCount.instance.RemoveCount(Action[Number]);
            if (ArrayAction == RhythmManager.RhythmAction.None)
            {
                ActionLevel.instance.AddLevel();                
            }
            else if(ArrayAction != RhythmManager.RhythmAction.None)
            {
                ActionCount.instance.AddCount(ArrayAction);
            }
        }
        else if(Action[Number] == RhythmManager.RhythmAction.None && ArrayAction != RhythmManager.RhythmAction.None)
        {
            ActionLevel.instance.RemoveLevel();
            ActionCount.instance.AddCount(ArrayAction);
        }
        Action[Number] = ArrayAction;
        SetRhythmManager();
    }

    public RhythmManager.RhythmAction GetMetronome(int Number)
    {
        return Action[Number];
    }

    public void ChangeNoteBox()
    {
        ActionCount.instance.ResetCount();
        for(int Number = 0;Number < 8;Number++)
        {
            GameObject Metronome = null;
            switch (Number)
            {
                case 0:
                    Metronome = GameObject.Find("frame_item0_1");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                case 1:
                    Metronome = GameObject.Find("frame_item0_2");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                case 2:
                    Metronome = GameObject.Find("frame_item0_3");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                case 3:
                    Metronome = GameObject.Find("frame_item0_4");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                case 4:
                    Metronome = GameObject.Find("frame_item0_5");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                case 5:
                    Metronome = GameObject.Find("frame_item0_6");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                case 6:
                    Metronome = GameObject.Find("frame_item0_7");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                case 7:
                    Metronome = GameObject.Find("frame_item0_8");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    ActionCount.instance.AddCount(Action[Number]);
                    break;

                default:
                    break;
            }
        }
    }

   public void ArrayAction(ActionFolder.ActionType ActionArray)
    {
        ActionCount.instance.ResetCount();
        for (int i = 0; i < 8; i++)
        {
            Action[i] = ActionArray.ActionArray[i];
            ActionCount.instance.AddCount(Action[i]);
        }
        SetRhythmManager();
    }

    public bool Ref_FirstScene()
    {
        return (FirstSet);
    }

}
