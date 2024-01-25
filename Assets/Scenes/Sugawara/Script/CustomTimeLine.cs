using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CustomTimeLine : MonoBehaviour
{
    public static CustomTimeLine instance = null;
    private PlayableDirector mDirector;
    private bool Stop = false;
    [SerializeField]private Animator[] mAnimators = new Animator[5];
    [SerializeField]private GameObject[] mGameObjects = new GameObject[2];
    int NowAnimation = 0;
    private int StopAnimation = 353;

    // Start is called before the first frame update
    void Awake()
    {
        NowAnimation = 0;
        instance = this;
        mDirector = this.GetComponent<PlayableDirector>();
        Stop = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        NowAnimation += 1;
        
        if (Stop == true)
        {
            NowAnimation = StopAnimation;
            mDirector.Stop();
            for(int i = 0; i < 5;i++)
            {
                mAnimators[i].enabled = false;
            }

            for (int i = 0; i < 2; i++)
            {
                mGameObjects[i].SetActive(false);
            }
        }

        if (NowAnimation >= StopAnimation)
        {
            Stop = true;
        }
    }

    public bool GetStopTimeLine()
    {
        return Stop;
    }

    
}
