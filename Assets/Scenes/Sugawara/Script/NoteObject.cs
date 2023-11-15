using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteObject : MonoBehaviour
{
    [SerializeField] RhythmManager.RhythmAction Action;
    // Start is called before the first frame update
    void Awake()
    {
        Action = RhythmManager.RhythmAction.None;
        this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAction(RhythmManager.RhythmAction Array)
    {
        switch(Array)
        {
            case RhythmManager.RhythmAction.Umbrella:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                break;

            case RhythmManager.RhythmAction.Recorder:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                break;

            case RhythmManager.RhythmAction.Eraser:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                break;

            case RhythmManager.RhythmAction.Sacrifice:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                break;

            case RhythmManager.RhythmAction.AirCannon:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                break;

            case RhythmManager.RhythmAction.Bag:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                break;

            case RhythmManager.RhythmAction.Ruler:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                break;

            case RhythmManager.RhythmAction.Whistle:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                break;

            case RhythmManager.RhythmAction.None:
                this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                break;
        }
    }
}
