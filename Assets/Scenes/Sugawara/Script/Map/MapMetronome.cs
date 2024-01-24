using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMetronome : MonoBehaviour
{
    public static MapMetronome instance;
    int MaxAction = 8;
    int MaxLevel = 9;
    [SerializeField] GameObject[] MetronomeObjects = new GameObject[8];
    [SerializeField] GameObject[] LevelObjects = new GameObject[9];
    bool Changer = false;

    //[SerializeField]int Level = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        string ObjectName = "None";

        for (int Number = 0; Number < MaxLevel; Number++)
        {
            if (Number < MaxAction)
            {
                ObjectName = "Map_frame_item0_";
                MetronomeObjects[Number] = GameObject.Find(ObjectName + (Number + 1));
            }

            ObjectName = "MapLevel";
            LevelObjects[Number] = GameObject.Find(ObjectName + (Number + 1));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Changer == true)
        {
            int Level = ActionLevel.instance.GetLevel();
            for (int LevelCount = 0; LevelCount < MaxLevel; LevelCount++)
            {
                LevelObjects[LevelCount].SetActive(false);
                if (LevelCount < Level)
                {
                    LevelObjects[LevelCount].SetActive(true);
                }
            }
            SetMetronome();
        }
    }

    void SetMetronome()
    {
        RhythmManager.RhythmAction InputAction = RhythmManager.RhythmAction.None;
        for (int MetroNumber = 0; MetroNumber < MaxAction; MetroNumber++)
        {
            InputAction = InputRhythm.instance.GetMetronome(MetroNumber);
            switch (InputAction)
            {
                case RhythmManager.RhythmAction.Umbrella:
                    MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                    break;

                case RhythmManager.RhythmAction.Recorder:
                    MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                    break;

                case RhythmManager.RhythmAction.Eraser:
                   MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                    break;

                case RhythmManager.RhythmAction.Sacrifice:
                   MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                    break;

                case RhythmManager.RhythmAction.AirCannon:
                   MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                    break;

                case RhythmManager.RhythmAction.Bag:
                   MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                    break;

                case RhythmManager.RhythmAction.Ruler:
                   MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                    break;

                case RhythmManager.RhythmAction.Whistle:
                   MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                    break;

                case RhythmManager.RhythmAction.None:
                   MetronomeObjects[MetroNumber].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                    break;

                default:
                    break;
            }
        }
    }

    public void SetUI()
    {
        Changer = true;
    }
}
