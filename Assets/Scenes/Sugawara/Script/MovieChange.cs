using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieChange : MonoBehaviour
{
    VideoPlayer player;
    [SerializeField] VideoClip[] ActionVideo = new VideoClip[9];
    [SerializeField] Texture[] ViewTexture = new Texture[3];
    [SerializeField] Material Material;
    [SerializeField] int MaxFlame = 60;
    [SerializeField] int ChangeFlame = 0;
    bool ChangeMovieFlag = false;
    bool SetMaterial  = false;

    void Awake()
    {
        player = this.GetComponent<VideoPlayer>();
        SetMaterial = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        player.clip = ActionVideo[2];
        ChangeMovieFlag = false;
        ChangeFlame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ChangeMovieFlag == true)
        {
            player.Play();
            ChangeMovieFlag = false;
        }

        if (SetMaterial == true)
        {
            ChangeFlame += 1;
            this.GetComponent<RawImage>().material.SetFloat("_BoolSwitch", 1.0f);
            this.GetComponent<RawImage>().material.EnableKeyword("FILL_WITH_RED");
            if (ChangeFlame > MaxFlame)
            {
                ChangeFlame = 0;
                SetMaterial = false;
                this.GetComponent<RawImage>().material.SetFloat("_BoolSwitch", 0.0f);
                this.GetComponent<RawImage>().material.DisableKeyword("FILL_WITH_RED");
            }
        }
    }

    public void Change(RhythmManager.RhythmAction Action)
    {
        switch(Action)
        {
            case RhythmManager.RhythmAction.Umbrella:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[0];
                ChangeMovieFlag = true;
                SetMaterial = true;
                this.GetComponent<RawImage>().material = Material;
                break;

            case RhythmManager.RhythmAction.Recorder:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[1];
                ChangeMovieFlag = true;
                SetMaterial = true;
                this.GetComponent<RawImage>().material = Material;
                break;

            case RhythmManager.RhythmAction.Eraser:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[2];
                ChangeMovieFlag = true;
                SetMaterial = true;
                this.GetComponent<RawImage>().material = Material;
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[3];
                ChangeMovieFlag = true;
                SetMaterial = true;
                this.GetComponent<RawImage>().material = Material;
                break;

            case RhythmManager.RhythmAction.AirCannon:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[4];
                ChangeMovieFlag = true;
                SetMaterial = true;
                this.GetComponent<RawImage>().material = Material;
                break;

            case RhythmManager.RhythmAction.Bag:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[5];
                ChangeMovieFlag = true;
                SetMaterial = true;
                this.GetComponent<RawImage>().material = Material;
                break;

            case RhythmManager.RhythmAction.Ruler:
                this.GetComponent<RawImage>().texture = ViewTexture[2];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Whistle:
                this.GetComponent<RawImage>().texture = ViewTexture[2];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.None:
                this.GetComponent<RawImage>().texture = ViewTexture[1];
                ChangeMovieFlag = true;
                SetMaterial = true;
                this.GetComponent<RawImage>().material = Material;
                break;

            default:
                this.GetComponent<RawImage>().texture = ViewTexture[2];
                ChangeMovieFlag = false;
                break;
        }
       
    }
}
