using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;
using UnityEngine;

public class ActionCount : MonoBehaviour
{
    public static ActionCount instance = null;    //シングルトン化
    [SerializeField] int[] Count = new int[9];       //アクションカウントを数える箱
    int MinCount = 0;
    int MaxCount = 3;
    int MaxAction = 9;

    void Awake()
    {
        instance = this;

        //カウント初期化
        for (int i = 0; i < MaxAction; i++) 
        {
            Count[i] = MinCount;
        }
    }
    
    public void ResetCount()
    {
        for (int i = 0; i < MaxAction; i++)
        {
            Count[i] = MinCount;
        }
    }

    public void AddCount(RhythmManager.RhythmAction ActionNumber)
    {
        if ((Count[(int)ActionNumber]) >= MaxCount)
        {
            return;
        }
        else
        {
            Count[(int)ActionNumber] += 1;
        }
    }

    public void RemoveCount(RhythmManager.RhythmAction ActionNumber)
    {
        if (Count[(int)ActionNumber] <= MinCount)
        {
            return;
        }
        else
        {
            Count[(int)ActionNumber] -= 1;
        }
    }

    public void SetCount(RhythmManager.RhythmAction ActionNumber, int CountNumber)
    {
        if (CountNumber >= MaxCount)
        {
            Count[(int)ActionNumber] = MaxCount;
            return;
        }
        else
        {
            Count[(int)ActionNumber] = CountNumber;
        }
    }

    public int GetCount(string Name)
    {
        switch (Name)
        {
            case ("Umbrella"):
                return Count[(int)RhythmManager.RhythmAction.Umbrella];

            case ("Recorder"):
                return Count[(int)RhythmManager.RhythmAction.Recorder];

            case ("Eraser"):
                return Count[(int)RhythmManager.RhythmAction.Eraser];

            case ("Sacrifice"):
                return Count[(int)RhythmManager.RhythmAction.Sacrifice];

            case ("AirCannon"):
                return Count[(int)RhythmManager.RhythmAction.AirCannon];

            case ("Bag"):
                return Count[(int)RhythmManager.RhythmAction.Bag];

            case ("Ruler"):
                return Count[(int)RhythmManager.RhythmAction.Ruler];

            case ("Whistle"):
                return Count[(int)RhythmManager.RhythmAction.Whistle];

            case ("None"):
                return Count[(int)RhythmManager.RhythmAction.None];

            default:
                break;
        }

        return Count[(int)RhythmManager.RhythmAction.None];
    }


}
