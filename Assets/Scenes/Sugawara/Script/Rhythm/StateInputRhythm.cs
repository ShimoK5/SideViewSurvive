using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateInputRhythm : MonoBehaviour
{
    public enum STATE{
        FirstSet = 0,
        TitleSet,
        Movie_01Set,
        Movie_02Set,
        NowGameSet,
        BreakSet,
        ResetSet,
    }

    public static StateInputRhythm instance;
    [SerializeField] STATE NowState;
    private string NowSceneName = null;
    private bool OneAction = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("更新");
        }
        else
        {
            //Destroy(gameObject);
        }
        NowState = STATE.FirstSet;
        OneAction = false;
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != NowSceneName)
        {
            NowSceneName = SceneManager.GetActiveScene().name;
            if (NowSceneName == "UI_title")
            {
                StateInputRhythm.instance.SetState(StateInputRhythm.STATE.TitleSet);
                ResetOneAction();
            }
            else if (NowSceneName == "01_Movie")
            {
                StateInputRhythm.instance.SetState(StateInputRhythm.STATE.Movie_01Set);
                ResetOneAction();
            }
            else if ((NowSceneName == "Game") || (NowSceneName == "Game Hard"))
            {
                StateInputRhythm.instance.SetState(StateInputRhythm.STATE.NowGameSet);
                ResetOneAction();
            }
            else if (NowSceneName == "SetScene")
            {
                if (NowState == StateInputRhythm.STATE.Movie_01Set)
                {
                    StateInputRhythm.instance.SetState(StateInputRhythm.STATE.FirstSet);
                }else if (NowState != StateInputRhythm.STATE.FirstSet)
                {
                    StateInputRhythm.instance.SetState(StateInputRhythm.STATE.BreakSet);
                }
                ResetOneAction();
            }
            else if (NowSceneName == "02_Movie")
            {
                StateInputRhythm.instance.SetState(StateInputRhythm.STATE.Movie_02Set);
                ResetOneAction();
            }
        }
    }

    public void SetState(STATE InputState)
    {
        NowState = InputState;
    }

    public STATE GetState()
    {
        if(OneAction == true)
        {
            OneAction = false;
        }
        return NowState;
    }

    public void ResetOneAction()
    {
        OneAction = true;
    }
    public bool GetOneAction()
    {
        return OneAction;
    }
}
