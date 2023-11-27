using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RhythmManager;

public class StrageNoteBox : MonoBehaviour
{
    [SerializeField] private GameObject NoteBox = null;
    [SerializeField] private RhythmManager Rhythm = null;
    [SerializeField] private static int PresetSpriteSize = 15;
    [SerializeField] private Sprite[] PresetSprite = new Sprite[PresetSpriteSize];
    [SerializeField] private AudioClip[] ActionSE = new AudioClip[PresetSpriteSize];
    [SerializeField] private AudioSource audioSource = null;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.03f;

        if (Rhythm == null)
        {
            GameObject StrageRhythmManager = GameObject.Find("RhythmManager");
            Rhythm = StrageRhythmManager.GetComponent<RhythmManager>();

            if (PresetSpriteSize != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    NoteBox = transform.GetChild(i).gameObject;

                    switch (Rhythm.ActionArray[i])
                    {
                        case RhythmAction.Umbrella:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[0];
                            break;

                        case RhythmAction.Recorder:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[1];
                            break;

                        case RhythmAction.Eraser:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[2];
                            break;

                        case RhythmAction.Sacrifice:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[3];
                            break;

                        case RhythmAction.AirCannon:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[4];
                            break;

                        case RhythmAction.Bag:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[5];
                            break;

                        case RhythmAction.Ruler:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[6];
                            break;

                        case RhythmAction.Whistle:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[7];
                            break;

                        default:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[8];
                            break;
                    }
                }

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Change();
        }

       
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
            int count = Rhythm.FCnt / Rhythm.BeatTempo;
            if (Rhythm.FCnt % Rhythm.BeatTempo == 0 && count == 0)
            {
                switch (Rhythm.ActionArray[7])
                {
                    case RhythmAction.Umbrella:
                        audioSource.PlayOneShot(ActionSE[0]);
                        break;

                    case RhythmAction.Recorder:
                        audioSource.PlayOneShot(ActionSE[1]);
                        break;

                    case RhythmAction.Eraser:
                        audioSource.PlayOneShot(ActionSE[2]);
                        break;

                    case RhythmAction.Sacrifice:
                        audioSource.PlayOneShot(ActionSE[3]);
                        break;

                    case RhythmAction.AirCannon:
                        audioSource.PlayOneShot(ActionSE[4]);
                        break;

                    case RhythmAction.Bag:
                        audioSource.PlayOneShot(ActionSE[5]);
                        break;

                    case RhythmAction.Ruler:
                        audioSource.PlayOneShot(ActionSE[6]);
                        break;

                    case RhythmAction.Whistle:
                        audioSource.PlayOneShot(ActionSE[7]);
                        break;

                    default:
                        audioSource.PlayOneShot(ActionSE[8]);
                        break;
                }
            }
            else if (Rhythm.FCnt % Rhythm.BeatTempo == 0 && count > 0)
            {
                switch (Rhythm.ActionArray[(count - 1)])
                {
                    case RhythmAction.Umbrella:
                        audioSource.PlayOneShot(ActionSE[0]);
                        break;

                    case RhythmAction.Recorder:
                        audioSource.PlayOneShot(ActionSE[1]);
                        break;

                    case RhythmAction.Eraser:
                        audioSource.PlayOneShot(ActionSE[2]);
                        break;

                    case RhythmAction.Sacrifice:
                        audioSource.PlayOneShot(ActionSE[3]);
                        break;

                    case RhythmAction.AirCannon:
                        audioSource.PlayOneShot(ActionSE[4]);
                        break;

                    case RhythmAction.Bag:
                        audioSource.PlayOneShot(ActionSE[5]);
                        break;

                    case RhythmAction.Ruler:
                        audioSource.PlayOneShot(ActionSE[6]);
                        break;

                    case RhythmAction.Whistle:
                        audioSource.PlayOneShot(ActionSE[7]);
                        break;

                    default:
                        audioSource.PlayOneShot(ActionSE[8]);
                        break;
                }
            }

                break;

            default:
                break;
        }
    }

    public void Change()
    {

        if (PresetSpriteSize != 0)
        {
            for (int i = 0; i < 8; i++)
            {
                NoteBox = transform.GetChild(i).gameObject;

                switch (Rhythm.ActionArray[i])
                {
                    case RhythmAction.Umbrella:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[0];
                        break;

                    case RhythmAction.Recorder:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[1];
                        break;

                    case RhythmAction.Eraser:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[2];
                        break;

                    case RhythmAction.Sacrifice:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[3];
                        break;

                    case RhythmAction.AirCannon:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[4];
                        break;

                    case RhythmAction.Bag:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[5];
                        break;

                    case RhythmAction.Ruler:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[6];
                        break;

                    case RhythmAction.Whistle:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[7];
                        break;

                    default:
                        NoteBox.GetComponent<Image>().sprite = PresetSprite[8];
                        break;
                }
            }

        }
        
    }
}
