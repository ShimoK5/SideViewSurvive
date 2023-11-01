using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieChange : MonoBehaviour
{
    VideoPlayer player;
    [SerializeField]VideoClip[] ActionVideo = new VideoClip[8];
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
                player.clip = ActionVideo[0];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Recorder:
                player.clip = ActionVideo[1];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Eraser:
                player.clip = ActionVideo[2];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                player.clip = ActionVideo[3];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.AirCannon:
                player.clip = ActionVideo[4];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Bag:
                player.clip = ActionVideo[5];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Ruler:
                player.clip = ActionVideo[6];
                ChangeMovieFlag = true;
                break;

            case RhythmManager.RhythmAction.Whistle:
                player.clip = ActionVideo[7];
                ChangeMovieFlag = true;
                break;

            default:
                ChangeMovieFlag = false;
                break;
        }
       
    }
}
