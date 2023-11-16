using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFlavor : MonoBehaviour
{
    public void Change(RhythmManager.RhythmAction Action)
    {
        this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceFlavor(Action);
    }

}
