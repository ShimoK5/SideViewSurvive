using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static NewSoundManager;

public class PlayActionSE : MonoBehaviour
{
    bool AssetJudge = false;

    public static PlayActionSE instance = null;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (NewSoundManager.instance.AS_SEdic["Umbrella"] == null)
        {
            AssetJudge = false;
        }
        else
        {
            AssetJudge = true;
        }
    }

    public void Ref_NewSoundManager(RhythmManager.RhythmAction Action)
    {
        if(AssetJudge == false)
        {
            return;
        }

        NewSoundManager.instance.ChangeVolume(1.0f, AS_TYPE.SE);
        NewSoundManager.instance.ChangeMute(true, AS_TYPE.BGM);
        switch (Action)
        {
            case RhythmManager.RhythmAction.Umbrella:
                NewSoundManager.instance.PlaySE("Umbrella");
                break;

            case RhythmManager.RhythmAction.Recorder:
                NewSoundManager.instance.PlaySE("Recorder");
                break;

            case RhythmManager.RhythmAction.Eraser:
                NewSoundManager.instance.PlaySE("Eraser");
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                NewSoundManager.instance.PlaySE("Sacrifice");
                break;

            case RhythmManager.RhythmAction.AirCannon:
                NewSoundManager.instance.PlaySE("AirCannon");
                break;

            case RhythmManager.RhythmAction.Bag:
                NewSoundManager.instance.PlaySE("Bag");
                break;

            case RhythmManager.RhythmAction.Ruler:
                NewSoundManager.instance.PlaySE("Ruler");
                break;

            case RhythmManager.RhythmAction.Whistle:
                NewSoundManager.instance.PlaySE("Whistle");
                break;

            case RhythmManager.RhythmAction.None:
                NewSoundManager.instance.PlaySE("None");
                break;
            default:
                NewSoundManager.instance.PlaySE("None");
                break;
        }
        
    }
    
}

