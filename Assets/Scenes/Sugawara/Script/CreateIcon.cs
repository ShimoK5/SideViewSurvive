using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CreateIcon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int HierarchyNumber = 3;                //どこのヒエラルキー階層に入れるか
    [SerializeField] GameObject PrefavIcon;                 //プレハブ化したオブジェクトを入れるよう
    [SerializeField] int ActionTypeCount = 8;
    void Start()
    {
        Vector3 PrefavePosition = this.transform.localPosition;     //各プレハブごとの座標
        float IconPosition = (this.GetComponent<RectTransform>().sizeDelta.x - (PrefavIcon.GetComponent<RectTransform>().sizeDelta.x)) / (ActionTypeCount - 1);   //各区切り幅を決めておく  
        GameObject CloneIconBox = null;                         //生成したオブジェクトを入れる用
        for (int SpriteSize = 0; SpriteSize < ActionTypeCount; SpriteSize++)
        {
            PrefavePosition.x = ((-this.GetComponent<RectTransform>().sizeDelta.x + (PrefavIcon.GetComponent<RectTransform>().sizeDelta.x)) / 2)
                + IconPosition * SpriteSize;
            CloneIconBox = Instantiate(PrefavIcon, PrefavePosition, Quaternion.identity);
            CloneIconBox.GetComponent<Image>().sprite = ReferenceAction(SpriteSize);
            CloneIconBox.tag = "Icon";
            CloneIconBox.name = "" +SpriteSize;
            CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
            CloneIconBox.transform.SetSiblingIndex(HierarchyNumber);
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Sprite ReferenceAction(int Action)
    {
        switch(Action)
        {
            case 0:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.Umbrella);

            case 1:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.Recorder);

            case 2:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.Eraser);

            case 3:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.Sacrifice);

            case 4:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.AirCannon);

            case 5:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.Bag);

            case 6:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.Ruler);

            case 7:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.Whistle);

            case 8:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.None);

            default:
                return AssetManager.Instance.ReferenceIcon(AssetManager.ActionName.None);

        }
    }
}
