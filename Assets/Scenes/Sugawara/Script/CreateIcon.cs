using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateIcon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int HierarchyNumber = 3;                //どこのヒエラルキー階層に入れるか
    [SerializeField] GameObject PrefavIcon;                 //プレハブ化したオブジェクトを入れるよう
    [SerializeField] Sprite[] IconSprite = default;         //アイコン画像保管用

    void Awake()
    {
        Vector3 PrefavePosition = this.transform.localPosition;     //各プレハブごとの座標
        float IconPosition = (this.GetComponent<RectTransform>().sizeDelta.x - (PrefavIcon.GetComponent<RectTransform>().sizeDelta.x)) / (IconSprite.Length - 1);   //各区切り幅を決めておく  
        GameObject CloneIconBox = null;                         //生成したオブジェクトを入れる用
        for (int SpriteSize = 0; SpriteSize < IconSprite.Length; SpriteSize++)
        {
            PrefavePosition.x = ((-this.GetComponent<RectTransform>().sizeDelta.x + (PrefavIcon.GetComponent<RectTransform>().sizeDelta.x)) / 2)
                + IconPosition * SpriteSize;
            CloneIconBox = Instantiate(PrefavIcon, PrefavePosition, Quaternion.identity);
            CloneIconBox.GetComponent<Image>().sprite = IconSprite[SpriteSize];
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
}
