using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputRhythm : MonoBehaviour
{
    public static  InputRhythm instance = null;                                                         //インスタンス保管庫
    [SerializeField] private bool Changer = false;                                                      //シーン変更の際に使用する変数
    public bool UpdateRhythmManager = false;                                                            //変更があった際に使用する変数
    [SerializeField] RhythmManager.RhythmAction[] Action = new RhythmManager.RhythmAction[8];           //インスペクター内でいじれるよう 
    private string SceneName = null;                                                                    //シーンネーム保管庫
    private bool FistGameChange = false;
    bool TitleCheck = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            ResetSceneAction();
            ChangeSceneAction();
        }

        for (int Number = 0; Number < 8; Number++)
        {
            Action[Number] = RhythmManager.RhythmAction.None;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name != SceneName)
        {
            ResetSceneAction();
            ChangeSceneAction();
            SceneName = SceneManager.GetActiveScene().name;
            FistGameChange = true;           
        }

        if (UpdateRhythmManager == true)
        {
            if (SceneManager.GetActiveScene().name == "ShimokawaraScene 1" || SceneManager.GetActiveScene().name == "SetScene" || SceneManager.GetActiveScene().name == "Game")
            {
                ChangeRhythmManager();
            }
            if (SceneManager.GetActiveScene().name == "SetScene")
            {
                if(TitleCheck == true)
                {
                    FistGameChange = false;
                    TitleCheck = false;
                }
                ChangeNoteBox();               
            }
            UpdateRhythmManager = false;
        }

        if (SceneManager.GetActiveScene().name == "ShimokawaraScene 1" || SceneManager.GetActiveScene().name == "Game")
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

        if (SceneManager.GetActiveScene().name == "UI_title")
        {
            if (TitleCheck == false)
            {
                ResetMetronome();
                TitleCheck = true;
            }
        }

        
    }

  

    void ChangeRhythmManager()
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

    void ResetMetronome()
    {
        for (int i = 0; i < 8; i++)
        {
            Action[i] = RhythmManager.RhythmAction.None;
        }
    }

    public void ChangeMetronome(int Number,RhythmManager.RhythmAction ArrayAction)
    {
        Action[Number] = ArrayAction;
    }

    public void ChangeNoteBox()
    {
        for(int Number = 0;Number < 8;Number++)
        {
            GameObject Metronome = null;
            switch (Number)
            {
                case 0:
                    Metronome = GameObject.Find("frame_item0_1");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                case 1:
                    Metronome = GameObject.Find("frame_item0_2");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                case 2:
                    Metronome = GameObject.Find("frame_item0_3");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                case 3:
                    Metronome = GameObject.Find("frame_item0_4");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                case 4:
                    Metronome = GameObject.Find("frame_item0_5");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                case 5:
                    Metronome = GameObject.Find("frame_item0_6");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                case 6:
                    Metronome = GameObject.Find("frame_item0_7");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                case 7:
                    Metronome = GameObject.Find("frame_item0_8");
                    Metronome.GetComponent<ChangeMetronome>().ChangeRestart(Action[Number]);
                    Metronome.GetComponent<ChangeMetronome>().ChangeSprite();
                    break;

                default:
                    break;
            }
        }
    }

   public void ArrayAction(ActionFolder.ActionType ActionArray)
    {
        for (int i = 0; i < 8; i++)
        {
            Action[i] = ActionArray.ActionArray[i];
        }
    }


    public void ChangeSceneAction()
    {
        if(Changer == false && UpdateRhythmManager == false)
        {
            Changer = true;
            UpdateRhythmManager = true;
        }
    }

    public void ResetSceneAction()
    {
        Changer = false;
        UpdateRhythmManager = false;
    }

    public bool Ref_FirstScene()
    {
        return (FistGameChange);
    }
}
