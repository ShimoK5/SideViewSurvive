using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleStateManager : MonoBehaviour
{
    public enum State {
    Normal,
    After
    }

    public static TitleStateManager instans;

    public int WAIT_MAX_CNT = 80;   //入力後の待ち時間
    public State state;

    int WaitCnt = 0;

    void Awake()
    {
        instans = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        WAIT_MAX_CNT = 80;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        { 
            case State.Normal:
                if (Input.anyKeyDown)
                {
                    state = State.After;
                }
                break;
            case State.After:
                
                break;
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                
                break;
            case State.After:
                WaitCnt++;
                if (WaitCnt == WAIT_MAX_CNT)
                {
                    SceneChangeManager.instance.SceneTransition("01_Movie");
                }
                break;
        }
    }
}
