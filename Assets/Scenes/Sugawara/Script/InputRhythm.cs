using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputRhythm : MonoBehaviour
{
    public static  InputRhythm instance = null;
    [SerializeField] private bool Changer = false;                                                      //シーン変更の際に使用する変数
    public bool UpdateRhythmManager = false;                                                            //変更があった際に使用する変数
    [SerializeField] RhythmManager.RhythmAction[] Action = new RhythmManager.RhythmAction[8];           //インスペクター内でいじれるよう
    public GameObject[] NoteBox = new GameObject[8];   //ノートボックス格納
    private string SceneName = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            for(int Number = 0; Number < 8;Number++)
            {
                NoteBox[Number] = null;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneName = SceneManager.GetActiveScene().name;
        for(int Number = 0;Number < 8;Number++)
        {
            Action[Number] = RhythmManager.RhythmAction.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != SceneName)
        {
            ResetSceneAction();
            ChangeSceneAction();
        }


        if (UpdateRhythmManager == true)
        {
            for(int Number = 0; Number < 8; Number++)
            {
                switch(Action[Number])
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

            UpdateRhythmManager = false;
        }
    }

    public void ChangeNoteBox(RhythmManager.RhythmAction ArrayAction,string NoteName)
    {
        int NoteNum = 0;
        switch (NoteName)
        {
            case "0":
                NoteNum = 0;
                break;

            case "1":
                NoteNum = 1;
                break;

            case "2":
                NoteNum = 2;
                break;

            case "3":
                NoteNum = 3;
                break;

            case "4":
                NoteNum = 4;
                break;

            case "5":
                NoteNum = 5;
                break;

            case "6":
                NoteNum = 6;
                break;

            case "7":
                NoteNum = 7;
                break;

            default: 
                return;
        }


        switch(ArrayAction)
        {
            case RhythmManager.RhythmAction.Umbrella:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.Umbrella); 
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Umbrella;
                Action[NoteNum] = RhythmManager.RhythmAction.Umbrella;
                break;

            case RhythmManager.RhythmAction.Recorder:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.Recorder);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Recorder;
                Action[NoteNum] = RhythmManager.RhythmAction.Recorder;
                break;

            case RhythmManager.RhythmAction.Eraser:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.Eraser);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Eraser;
                Action[NoteNum] = RhythmManager.RhythmAction.Eraser;
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.Sacrifice);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Sacrifice;
                Action[NoteNum] = RhythmManager.RhythmAction.Sacrifice;
                break;

            case RhythmManager.RhythmAction.AirCannon:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.AirCannon);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.AirCannon;
                Action[NoteNum] = RhythmManager.RhythmAction.AirCannon;
                break;

            case RhythmManager.RhythmAction.Bag:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.Bag);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Bag;
                Action[NoteNum] = RhythmManager.RhythmAction.Bag;
                break;

            case RhythmManager.RhythmAction.Ruler:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.Ruler);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Ruler;
                Action[NoteNum] = RhythmManager.RhythmAction.Ruler;
                break;

            case RhythmManager.RhythmAction.Whistle:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.Whistle);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Whistle;
                Action[NoteNum] = RhythmManager.RhythmAction.Whistle;
                break;

            case RhythmManager.RhythmAction.None:
                NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.None);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.None;
                Action[NoteNum] = RhythmManager.RhythmAction.None;
                break;

            default:
                if (NoteNum >= 0 && NoteNum <= 7)
                {
                    NoteBox[NoteNum].GetComponent<NoteObject>().ChangeAction(RhythmManager.RhythmAction.None);
                    RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.None;
                    Action[NoteNum] = RhythmManager.RhythmAction.None;
                }
                else
                {
                    Debug.Log("どこのノートなの？");
                    break;
                }
                break;

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
}
