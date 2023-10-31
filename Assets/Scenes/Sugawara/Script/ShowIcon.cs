using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIcon : MonoBehaviour
{
    [SerializeField] Sprite[] IconSprite = new Sprite[13];
    [SerializeField] GameObject NextIcon = null;
    [SerializeField] GameObject NowIcon = null;
    int FCnt = 0;
    bool FirstAction = false;

    // Start is called before the first frame update
    void Start()
    {
        NowIcon = this.transform.Find("NowAction").gameObject;
        NextIcon = this.transform.Find("NextAction").gameObject;
        FCnt = 0;
        FirstAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        FCnt = RhythmManager.Instance.FCnt;

        if(FCnt % RhythmManager.Instance.BeatTempo == 0 &&  FCnt / RhythmManager.Instance.BeatTempo == 0)
        {
            if(FirstAction == false)
            {
                FirstAction = true;
                NowIcon.GetComponent<Image>().sprite = IconSprite[8];
            }
            else
            {
                ReferenceAction(7);
            }
        }
        else if(FCnt % RhythmManager.Instance.BeatTempo == 0 && FCnt / RhythmManager.Instance.BeatTempo > 0)
        {
            int ActionNumber = FCnt / RhythmManager.Instance.BeatTempo;
            ReferenceAction(ActionNumber - 1);
        }
    }

    void ReferenceAction(int ActionNumber)
    {
        switch(RhythmManager.Instance.ActionArray[ActionNumber])
        {
            case RhythmManager.RhythmAction.Umbrella:
                
                NowIcon.GetComponent<Image>().sprite = IconSprite[0];
                    break;

            case RhythmManager.RhythmAction.Recorder:

                NowIcon.GetComponent<Image>().sprite = IconSprite[1];
                break;

            case RhythmManager.RhythmAction.Eraser:

                NowIcon.GetComponent<Image>().sprite = IconSprite[2];
                break;

            case RhythmManager.RhythmAction.Sacrifice:

                NowIcon.GetComponent<Image>().sprite = IconSprite[3];
                break;

            case RhythmManager.RhythmAction.AirCannon:

                NowIcon.GetComponent<Image>().sprite = IconSprite[4];
                break;

            case RhythmManager.RhythmAction.Bag:

                NowIcon.GetComponent<Image>().sprite = IconSprite[5];
                break;

            case RhythmManager.RhythmAction.Ruler:

                NowIcon.GetComponent<Image>().sprite = IconSprite[6];
                break;

            case RhythmManager.RhythmAction.Whistle:

                NowIcon.GetComponent<Image>().sprite = IconSprite[7];
                break;

            default:

                NowIcon.GetComponent<Image>().sprite = IconSprite[9];
                break;
        }

        if(ActionNumber == 7)
        {
            ActionNumber = -1;
        }

        switch (RhythmManager.Instance.ActionArray[ActionNumber + 1])
        {
            case RhythmManager.RhythmAction.Umbrella:

                NextIcon.GetComponent<Image>().sprite = IconSprite[0];
                break;

            case RhythmManager.RhythmAction.Recorder:

                NextIcon.GetComponent<Image>().sprite = IconSprite[1];
                break;

            case RhythmManager.RhythmAction.Eraser:

                NextIcon.GetComponent<Image>().sprite = IconSprite[2];
                break;

            case RhythmManager.RhythmAction.Sacrifice:

                NextIcon.GetComponent<Image>().sprite = IconSprite[3];
                break;

            case RhythmManager.RhythmAction.AirCannon:

                NextIcon.GetComponent<Image>().sprite = IconSprite[4];
                break;

            case RhythmManager.RhythmAction.Bag:

                NextIcon.GetComponent<Image>().sprite = IconSprite[5];
                break;

            case RhythmManager.RhythmAction.Ruler:

                NextIcon.GetComponent<Image>().sprite = IconSprite[6];
                break;

            case RhythmManager.RhythmAction.Whistle:

                NextIcon.GetComponent<Image>().sprite = IconSprite[7];
                break;

            default:

                NextIcon.GetComponent<Image>().sprite = IconSprite[9];
                break;
        }
    }
}
