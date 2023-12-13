﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Data.Util.KeywordDependentCollection;

public class Change_NoteBox : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
    }

    public void ChangeSprite(RhythmManager.RhythmAction action,Sprite ActionSprite)
    {
        switch(action)
        {
            case RhythmManager.RhythmAction.Umbrella:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.Recorder:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.Eraser:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.AirCannon:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.Bag:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.Ruler:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.Whistle:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            case RhythmManager.RhythmAction.None:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;

            default:
                this.gameObject.GetComponent<Image>().sprite = ActionSprite;
                break;
        }
    }
}
