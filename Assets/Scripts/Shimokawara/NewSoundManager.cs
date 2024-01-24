using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSoundManager : MonoBehaviour
{
    [Serializable]
    public class StringAudioClipKeyValuePair : SerializableKeyValuePair<string, AudioClip> { }

    [Serializable]
    public class Dic_AudioSource : SerializableDictionary<string, AudioClip, StringAudioClipKeyValuePair> { }

    public Dic_AudioSource AS_BGMdic;
    public Dic_AudioSource AS_SEdic;

    [SerializeField] AudioSource AS_BGM;
    [SerializeField] AudioSource AS_SE;

    public static NewSoundManager instance;

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
        //instance = this;
    }

    public void PlayBGM(string key, bool isloop = true)
    {
        Keynull(key, AS_TYPE.BGM);

        // 既に同じものが入っていたら何もしない
        if(AS_BGM.clip == AS_BGMdic[key]) { return; }

        // 鳴っていたら止める
        if(AS_BGM.isPlaying) { AS_BGM.Stop(); }

        // クリップ登録
        AS_BGM.clip = AS_BGMdic[key];
        AS_BGM.loop = isloop;

        AS_BGM.Play();
        //ピッチを1に初期化
        AS_BGM.pitch = 1;
    }

    public void StopBGM(/*string key*/) 
    {
        //Debug.Log("StopBGM");
        //Keynull(key, AS_TYPE.BGM);

        
        AS_BGM.Stop();
        AS_BGM.clip = null;
        //Debug.Log("StopBGM");
    }

    public void PlaySE(string key)
    {
        Keynull(key, AS_TYPE.SE);

        AS_SE.PlayOneShot(AS_SEdic[key]);
    }

    public void ChangeVolume(float volume, AS_TYPE type)
    {
        // BGM音量変更
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

    public string GetBGMName()
    {
        return AS_BGM.clip.name;
    }
    public float GetBGMTime()
    {
        return AS_BGM.time;
    }
    public float GetBGMLength()
    {
        return AS_BGM.clip.length;
    }

    public void SetBGMPitch(float pitch)
    {
        AS_BGM.pitch = pitch;
    }

    public float GetPitch()
    {
        return AS_BGM.pitch;
    }

}
