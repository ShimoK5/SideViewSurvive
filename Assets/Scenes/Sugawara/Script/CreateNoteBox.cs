using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CreateNoteBox : MonoBehaviour
{
    [SerializeField] GameObject PrefabNote = null;                  //どのプレハブをノートとして使うか
    [SerializeField] GameObject[] SelectNote = new GameObject[8];   //ノートを8つ分作成するよう
    [SerializeField] int HierarchyNumber = 3;                       //ヒエラルキーの場所指定
    Color ImageColor = Color.white;                                 //アルファ値変更用

    void Start()
    {
        Vector3 PrefabPosition = this.transform.localPosition;
        float MoveWidth = PrefabPosition.x;
        float NotePosition = (this.GetComponent<RectTransform>().sizeDelta.x - PrefabNote.GetComponent<RectTransform>().sizeDelta.x  - 10.0f ) / 7;
        GameObject CloneNote = null;
        ImageColor.a = 0.5f;
        for (int NoteNum = 0; NoteNum < SelectNote.Length; NoteNum++)
        {
            PrefabPosition.x = (-(this.GetComponent<RectTransform>().sizeDelta.x) + PrefabNote.GetComponent<RectTransform>().sizeDelta.x + 5.0f) / 2
                + NotePosition * NoteNum + MoveWidth;
            CloneNote = Instantiate(PrefabNote, PrefabPosition, Quaternion.identity);
            CloneNote.name = "" + NoteNum;
            CloneNote.tag = "Note";
            CloneNote.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceBox(RhythmManager.Instance.ActionArray[NoteNum]);
            CloneNote.GetComponent<Image>().color = ImageColor;
            CloneNote.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
            CloneNote.transform.SetSiblingIndex(HierarchyNumber);
            InputRhythm.instance.NoteBox[NoteNum] = CloneNote;
        }
        //Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
