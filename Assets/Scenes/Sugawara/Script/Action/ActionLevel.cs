using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLevel : MonoBehaviour
{
    public static ActionLevel instance;
    int MaxAction = 8;
    int MaxLevel = 9;
    int MinLevel = 1;
    [SerializeField]int Count = 0;
    [SerializeField] GameObject[] LevelObject = new GameObject[9];
    [SerializeField]bool SetLevelActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Count = 0;
        
        for(int LevelNumber = 0; LevelNumber < MaxLevel;LevelNumber++)
        {
            string ObjectName = "Level";
            LevelObject[LevelNumber] =GameObject.Find(ObjectName + (LevelNumber + 1));
        }
        SetLevelActive = false;
    }

    void FixedUpdate()
    {
        if (SetLevelActive == true)
        {
            ResetLevel();
            for (int LevelCount = 0; LevelCount < Count; LevelCount++)
            {
                LevelObject[(MaxLevel - LevelCount - 1)].SetActive(false);
            }
            SetLevelActive = false;
        }
    }

    void ResetLevel()
    {
        for (int LevelCount = 0; LevelCount < MaxAction ; LevelCount++)
        {
            LevelObject[LevelCount].SetActive(true);
        }
    }

    public void SetLevel()
    {
        Count = 0;
        for (int ActionCount = 0; ActionCount < MaxAction; ActionCount++)
        {
            if (InputRhythm.instance.GetMetronome(ActionCount) != RhythmManager.RhythmAction.None)
            {
                Count++;
            }
        }
        SetLevelActive = true;
    }

    public int GetLevel()
    {
        return (MaxLevel - Count);
    }

    public void RemoveLevel()
    {
        Count++;
        if(Count >= MaxLevel)
        {
            Count = MaxLevel ;
        }
        SetLevelActive = true;
    }

    public void AddLevel()
    {
        Count--;
        if(Count < MinLevel)
        {
            Count = MinLevel;
        }
        SetLevelActive = true;
    }
}
