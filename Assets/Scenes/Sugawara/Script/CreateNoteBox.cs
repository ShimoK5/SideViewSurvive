using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CreateNoteBox : MonoBehaviour
{
    [SerializeField] GameObject PrefabNote = null;
    [SerializeField] GameObject[] SelectNote = new GameObject[8];
    [SerializeField] int HierarchyNumber = 3;   

    void Start()
    {
        Vector3 PrefabPosition = this.transform.localPosition;
        float NotePosition = (this.GetComponent<RectTransform>().sizeDelta.x - PrefabNote.GetComponent<RectTransform>().sizeDelta.x * 2 - 20.0f) / 7;
        GameObject CloneNote = null;
        for (int NoteNum = 0; NoteNum < SelectNote.Length; NoteNum++)
        {
            PrefabPosition.x = (-(this.GetComponent<RectTransform>().sizeDelta.x) + PrefabNote.GetComponent<RectTransform>().sizeDelta.x * 2 + 20.0f) / 2
                + NotePosition * NoteNum;
            CloneNote = Instantiate(PrefabNote, PrefabPosition, Quaternion.identity);
            CloneNote.name = "Note" + (NoteNum + 1);
            CloneNote.tag = "Note";
            CloneNote.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
            CloneNote.transform.SetSiblingIndex(HierarchyNumber);
            MoveHand.instance.NoteBox[NoteNum] = CloneNote;
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixUpdate()
    {
        
    }
}
