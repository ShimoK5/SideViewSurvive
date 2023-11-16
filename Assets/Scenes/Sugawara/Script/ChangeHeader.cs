using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHeader : MonoBehaviour
{
    public void Change(RhythmManager.RhythmAction Action)
    {
        this.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceHeader(Action);
    }
}
