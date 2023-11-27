using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;
using System;


public class BGMPlayer : MonoBehaviour
{
    public string selectplayTitle;
    public bool isloop;
    public bool StartPlayBGM;

    BGMPlayer()
    {

    }
    //~BGMPlayer()
    //{
    //    NewSoundManager.instance.StopBGM(selectplayTitle);
    //}

    // Start is called before the first frame update
    void Start()
    {
        if(StartPlayBGM)
        {
            BGMPlaying();
        }
    }

    void OnDestroy()
    {
        if(NewSoundManager.instance)
        NewSoundManager.instance.StopBGM(selectplayTitle);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BGMPlaying()
    {
        NewSoundManager.instance.PlayBGM(selectplayTitle, isloop);
    }
}
