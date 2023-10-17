using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RhythmManager;

public class StrageNoteBox : MonoBehaviour
{
    [SerializeField] private GameObject NoteBox = null;
    [SerializeField] private RhythmManager Rhythm = null;
    [SerializeField] private static int PresetSpriteSize = 5;
    [SerializeField] private Sprite[] PresetSprite = new Sprite[PresetSpriteSize];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Rhythm == null)
        {
            GameObject StrageRhythmManager = GameObject.Find("RhythmManager");
            Rhythm = StrageRhythmManager.GetComponent<RhythmManager>();

            if (PresetSpriteSize != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    NoteBox = transform.GetChild(i).gameObject;

                    switch(Rhythm.ActionArray[i])
                    {
                        case RhythmAction.GymClothes:
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


                        case RhythmAction.None:
                            NoteBox.GetComponent<Image>().sprite = PresetSprite[4];


                            break;

                        default:
                            break;
                    }                 
                }

            }
        }
        
    }
}
