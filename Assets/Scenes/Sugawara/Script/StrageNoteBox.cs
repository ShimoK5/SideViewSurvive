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
    //[SerializeField] private AudioClip[] ActionSE = new AudioClip[PresetSpriteSize];
    //[SerializeField] private AudioSource audioSource = null;


    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.volume = 0.03f;

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
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[0]);
                            break;

                        case RhythmAction.Recorder:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[1]);
                            break;

                        case RhythmAction.Eraser:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[2]);
                            break;

                        case RhythmAction.Sacrifice:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[3]);
                            break;

                        case RhythmAction.AirCannon:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[4]);
                            break;

                        case RhythmAction.Bag:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[5]);
                            break;

                        case RhythmAction.Ruler:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[6]);
                            break;

                        case RhythmAction.Whistle:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[7]);
                            break;

                        case RhythmAction.None:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[8]);
                            break;

                        default:
                            NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[9]);
                            break;
                    }
                }

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
            int count = Rhythm.FCnt / Rhythm.BeatTempo;
                if (Rhythm.FCnt % Rhythm.BeatTempo == 0 && count == 0)
                {
                    PlayActionSE.instance.Ref_NewSoundManager(Rhythm.ActionArray[7]);
                }
                else if (Rhythm.FCnt % Rhythm.BeatTempo == 0 && count > 0)
                {
                    PlayActionSE.instance.Ref_NewSoundManager(Rhythm.ActionArray[(count - 1)]);                   
                }

                break;

            default:
                break;
        }
    }

    public void Change()
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
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[0]);
                        break;

                    case RhythmAction.Recorder:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[1]);
                        break;

                    case RhythmAction.Eraser:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[2]);
                        break;

                    case RhythmAction.Sacrifice:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[3]);
                        break;

                    case RhythmAction.AirCannon:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[4]);
                        break;

                    case RhythmAction.Bag:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[5]);
                        break;

                    case RhythmAction.Ruler:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[6]);
                        break;

                    case RhythmAction.Whistle:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[7]);
                        break;

                    case RhythmAction.None:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[8]);
                        break;

                    default:
                        NoteBox.GetComponent<Change_NoteBox>().ChangeSprite(Rhythm.ActionArray[i], PresetSprite[9]);
                        break;
                }
            }

        }
        
    }
}
