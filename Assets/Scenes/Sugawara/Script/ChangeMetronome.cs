using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMetronome : MonoBehaviour
{
    [SerializeField] int MetronomeNumber;
    [SerializeField] Sprite NowActionSprite = null;
    bool changesprite = false;
    // Start is called before the first frame update
    void Start()
    {
        NowActionSprite = this.GetComponent<Image>().sprite;
        changesprite = false;
    }

    public void StorageIcon(string Name)
    {
        switch (Name)
        {
            case ("Umbrella"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.Umbrella);
                break;

            case ("Recorder"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.Recorder);
                break;

            case ("Eraser"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.Eraser);
                break;

            case ("Sacrifice"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.Sacrifice);
                break;

            case ("AirCannon"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.AirCannon);
                break;

            case ("Bag"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.Bag);
                break;

            case ("Ruler"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;

            case ("Whistle"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;

            case ("None"):
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;

            default:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                NowActionSprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                InputRhythm.instance.SetMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;
        }
    }

    public void ChangeRestart(RhythmManager.RhythmAction arrayAction)
    {
        switch (arrayAction)
        {
            case RhythmManager.RhythmAction.Umbrella:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                break;

            case RhythmManager.RhythmAction.Recorder:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                break;

            case RhythmManager.RhythmAction.Eraser:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                break;

            case RhythmManager.RhythmAction.AirCannon:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                break;

            case RhythmManager.RhythmAction.Bag:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                break;

            case RhythmManager.RhythmAction.Ruler:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                break;

            case RhythmManager.RhythmAction.Whistle:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                break;

            case RhythmManager.RhythmAction.None:
                this.gameObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                break;

            default:
                break;
        }        
    }

    public void ChangeSprite()
    {
        NowActionSprite = this.GetComponent<Image>().sprite;
    }

    public void TouchMetronome()
    {
        if (changesprite == false)
        {
            Debug.Log("touch" + MetronomeNumber);
            NowActionSprite = this.GetComponent<Image>().sprite;
            //this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
            changesprite = true;
        }
    }

    public void DontTouchMetronome()
    {
        if (changesprite == true)
        {
            Debug.Log("donttouch" + MetronomeNumber);
            this.GetComponent<Image>().sprite = NowActionSprite;
            changesprite = false;
        }
    }
}
