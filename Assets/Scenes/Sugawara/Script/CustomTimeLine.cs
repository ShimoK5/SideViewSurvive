using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CustomTimeLine : MonoBehaviour
{
    public static CustomTimeLine instance = null;
    private PlayableDirector mDirector;
    private bool Stop = false;
    [SerializeField]private Animator mAnimator;
    [SerializeField]private GameObject mGameObject;
    int NowAnimation = 0;
    private int StopAnimation = 360;

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

        if(NowAnimation >= StopAnimation)
        {
            Stop = true;
        }
        if (Stop == true)
        {
            NowAnimation = StopAnimation;
            mDirector.Stop();
            mAnimator.enabled = false;
            mGameObject.SetActive(false);
        }
    }

    //public void StopTimeLine()
    //{
    //    Stop = true;
    //}
}
