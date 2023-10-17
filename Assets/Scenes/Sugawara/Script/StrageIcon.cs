using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrageIcon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject PrefavIcon;                 //プレハブ化したオブジェクトを入れるよう
    [SerializeField] Sprite[] IconSprite = default;         //アイコン画像保管用
    Vector3 PrefavePosition = Vector3.zero;                 //各プレハブごとの座標
    GameObject CloneIconBox = null;                         //生成したオブジェクトを入れる用

    void Start()
    {
        PrefavePosition = this.transform.localPosition;         
        int size = (IconSprite.Length/2);       
        RectTransform IconBoxTransform = this.GetComponent<RectTransform>();
        float IconPosition = (IconBoxTransform.sizeDelta.x /  2.0f )/ (1.0f+(float)size);
        float MovePosition = IconPosition;

        if((IconSprite.Length %2) == 1)
        {

            for (int i = 0; i<size;i++)
            { 
                if(i == 0)
                {
                    CloneIconBox = Instantiate(PrefavIcon, PrefavePosition, Quaternion.identity);
                    CloneIconBox.GetComponent<Image>().sprite = IconSprite[size];
                    CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);                  
                    CloneIconBox.transform.SetSiblingIndex(5);
                }
                PrefavePosition.x = IconPosition;
                CloneIconBox = Instantiate(PrefavIcon, PrefavePosition, Quaternion.identity);
                CloneIconBox.GetComponent<Image>().sprite = IconSprite[i];
                CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);               
                CloneIconBox.transform.SetSiblingIndex(5);

                PrefavePosition.x = -IconPosition;
                CloneIconBox = Instantiate(PrefavIcon, PrefavePosition, Quaternion.identity);
                CloneIconBox.GetComponent<Image>().sprite = IconSprite[size + i + 1];
                CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);               
                CloneIconBox.transform.SetSiblingIndex(5);

                IconPosition += MovePosition;
               

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
