using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFolder : MonoBehaviour
{

    [System.Serializable]
    public class ActionType
    {
        public RhythmManager.RhythmAction[] ActionArray = new RhythmManager.RhythmAction[8];
    }

    public ActionType[] TypeArray = new ActionType[2];

    public static ActionFolder instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ActionType GetAction(int Number)
    {
        return instance.TypeArray[Number];
    }
}
