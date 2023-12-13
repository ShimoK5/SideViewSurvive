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

    public GameObject DefaultAnim;
    public GameObject FastAnim;

    public static TitleStateManager instans;

    public int WAIT_MAX_CNT = 80;   //入力後の待ち時間
    public State state;

    int WaitCnt = 0;

    int SPAWN_COOL_TIME = 68;
    int SpawnFCnt = 0;

    void Awake()
    {
        instans = this;
        DefaultAnim.SetActive(false);
        FastAnim.SetActive(false);

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
                if(!SceneChangeManager.instance.isFade)
                {
                    if (Input.anyKeyDown)
                    {
                        NewSoundManager.instance.PlaySE("仮SE");
                        state = State.After;
                    }
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
                {
                    SpawnFCnt++;
                    if(SpawnFCnt > SPAWN_COOL_TIME)
                    {
                        DefaultAnim.SetActive(true);
                    }
                    else
                    {
                        DefaultAnim.SetActive(false);
                    }
                    
                    FastAnim.SetActive(false);
                }
                break;
            case State.After:
                WaitCnt++;

                {
                    DefaultAnim.SetActive(false);
                    FastAnim.SetActive(true);
                }

                if (WaitCnt == WAIT_MAX_CNT)
                {
                    SceneChangeManager.instance.SceneTransition("01_Movie");
                }
                break;
        }
    }
}
