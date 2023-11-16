using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieChange : MonoBehaviour
{
    VideoPlayer player;
    [SerializeField] VideoClip[] ActionVideo = new VideoClip[9];
    [SerializeField] Texture[] ViewTexture = new Texture[2];
    bool ChangeMovieFlag = false;

    void Awake()
    {
        player = this.GetComponent<VideoPlayer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player.clip = ActionVideo[0];
        ChangeMovieFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ChangeMovieFlag == true)
        {
            player.Play();
            ChangeMovieFlag = false;
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
                break;

            case RhythmManager.RhythmAction.Recorder:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[1];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Eraser:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[2];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[3];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.AirCannon:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[4];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Bag:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[5];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Ruler:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[6];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Whistle:
                this.GetComponent<RawImage>().texture = ViewTexture[0];
                player.clip = ActionVideo[7];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.None:
                this.GetComponent<RawImage>().texture = ViewTexture[1];
                ChangeMovieFlag = true;
                break;

            default:
                this.GetComponent<RawImage>().texture = ViewTexture[1];
                ChangeMovieFlag = false;
                break;
        }
       
    }
}
