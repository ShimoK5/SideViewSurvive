using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    bool PlaySound = false;
    void Awake()
    {
        PlaySound = false;
    }

    void FixedUpdate()
    {
        if (PlaySound == false)
        {
            NewSoundManager.instance.PlayBGM("セットBGM");
            PlaySound = true;
        }
    }
}
