using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrageIcon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int HierarchyNumber = 3;                //どこのヒエラルキー階層に入れるか
    [SerializeField] GameObject PrefavIcon;                 //プレハブ化したオブジェクトを入れるよう
    [SerializeField] Sprite[] IconSprite = default;         //アイコン画像保管用
    GameObject CloneIconBox = null;                         //生成したオブジェクトを入れる用

    void Start()
    {
        Vector3 PrefavePosition = this.transform.localPosition;     //各プレハブごとの座標
        float IconPosition = (this.GetComponent<RectTransform>().sizeDelta.x - (PrefavIcon.GetComponent<RectTransform>().sizeDelta.x)) / (IconSprite.Length - 1);
        float MovePosition = IconPosition;     

        for (int i = 0; i < IconSprite.Length; i++)
        {

            PrefavePosition.x = ((-this.GetComponent<RectTransform>().sizeDelta.x + (PrefavIcon.GetComponent<RectTransform>().sizeDelta.x)) / 2)
                + IconPosition * i;
            CloneIconBox = Instantiate(PrefavIcon, PrefavePosition, Quaternion.identity);
            CloneIconBox.GetComponent<Image>().sprite = IconSprite[i];
            CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
            CloneIconBox.transform.SetSiblingIndex(HierarchyNumber);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
