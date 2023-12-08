using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static Data.Util.KeywordDependentCollection;

public class CustomTimeLine : MonoBehaviour
{
    public static CustomTimeLine instance = null;
    private PlayableDirector mDirector;
    private bool Stop = false;
    [SerializeField]private Animator mAnimator;
    [SerializeField]private GameObject mGameObject;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        mDirector = this.GetComponent<PlayableDirector>();
        Stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Stop == true)
        {
            mDirector.Stop();
            mAnimator.enabled = false;
            mGameObject.SetActive(false);
        }
    }

    public void StopTimeLine()
    {
        Stop = true;
    }
}
