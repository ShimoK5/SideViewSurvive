using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    bool PlaySound = false;
    [SerializeField]string PlayKey;
    void Awake()
    {
        PlaySound = false;
    }

    void FixedUpdate()
    {
        if (PlaySound == false)
        {
            NewSoundManager.instance.PlayBGM(PlayKey);
            PlaySound = true;
        }
    }
}
