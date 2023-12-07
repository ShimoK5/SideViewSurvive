using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMetronome : MonoBehaviour
{
    [SerializeField] GameObject ChildObject = null;
    [SerializeField] int MetronomeNumber;
    // Start is called before the first frame update
    void Start()
    {
        ChildObject = this.transform.GetChild(0).gameObject;
    }

    public void StorageIcon(string Name)
    {
        switch (Name)
        {
            case ("Umbrella"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.Umbrella);
                break;

            case ("Recorder"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.Recorder);
                break;

            case ("Eraser"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.Eraser);
                break;

            case ("Sacrifice"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.Sacrifice);
                break;

            case ("AirCannon"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.AirCannon);
                break;

            case ("Bag"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.Bag);
                break;

            case ("Ruler"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;

            case ("Whistle"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;

            case ("None"):
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;

            default:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                InputRhythm.instance.ChangeMetronome(MetronomeNumber, RhythmManager.RhythmAction.None);
                break;
        }
    }

    public void ChangeRestart(RhythmManager.RhythmAction arrayAction)
    {
        switch (arrayAction)
        {
            case RhythmManager.RhythmAction.Umbrella:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                break;

            case RhythmManager.RhythmAction.Recorder:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                break;

            case RhythmManager.RhythmAction.Eraser:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                break;

            case RhythmManager.RhythmAction.AirCannon:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                break;

            case RhythmManager.RhythmAction.Bag:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                break;

            case RhythmManager.RhythmAction.Ruler:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                break;

            case RhythmManager.RhythmAction.Whistle:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                break;

            case RhythmManager.RhythmAction.None:
                ChildObject.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                break;

            default:
                break;
        }        
    }
}
