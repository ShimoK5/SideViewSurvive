using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSoundManager : MonoBehaviour
{
    [Serializable]
    public class StringAudioClipKeyValuePair : SerializableKeyValuePair<string, AudioClip> { }

    [Serializable]
    public class Dic_AudioSource : SerializableDictionary<string, AudioClip, StringAudioClipKeyValuePair> { }

    public Dic_AudioSource AS_BGMdic;
    public Dic_AudioSource AS_SEdic;

    [SerializeField] AudioSource AS_BGM;
    [SerializeField] AudioSource AS_SE;

    public static OldSoundManager instance;

    public enum AS_TYPE
    {
        BGM = 0,
        SE,
    };

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlayBGM(string key, bool isloop = true)
    {
        Keynull(key, AS_TYPE.BGM);

        // ä˘Ç…ìØÇ∂Ç‡ÇÃÇ™ì¸Ç¡ÇƒÇ¢ÇΩÇÁâΩÇ‡ÇµÇ»Ç¢
        if(AS_BGM.clip == AS_BGMdic[key]) { return; }

        // ñ¬Ç¡ÇƒÇ¢ÇΩÇÁé~ÇﬂÇÈ
        if(AS_BGM.isPlaying) { AS_BGM.Stop(); }

        // ÉNÉäÉbÉvìoò^
        AS_BGM.clip = AS_BGMdic[key];
        AS_BGM.loop = isloop;

        AS_BGM.Play();
    }

    public void StopBGM(string key) 
    {
        Keynull(key, AS_TYPE.BGM);

        AS_BGM.Stop();
    }

    public void PlaySE(string key)
    {
        Keynull(key, AS_TYPE.SE);

        AS_SE.PlayOneShot(AS_SEdic[key]);
    }

    public void ChangeVolume(float volume, AS_TYPE type)
    {
        // BGMâπó ïœçX
        switch(type)
        {
            case AS_TYPE.BGM:
                AS_BGM.volume = volume;
                break;

            case AS_TYPE.SE:
                AS_SE.volume = volume;
                break;
        }
    }

    public void ChangeMute(bool mute, AS_TYPE type)
    {
        switch (type)
        {
            case AS_TYPE.BGM:
                AS_BGM.mute = mute;
                break;

           case AS_TYPE.SE:
                AS_SE.mute = mute;
                break;
        }
    }

    void Keynull(string key, AS_TYPE type)
    {
        if (type == AS_TYPE.BGM)
        {
            if (AS_BGMdic[key] == null) {
                Debug.Log("BGM Notting");    
                return; 
            }
        }

        if (type == AS_TYPE.SE)
        {
            if (AS_SEdic[key] == null) 
            {
                Debug.Log("SE Notting");
                return; 
            }
        }
    }
}
