using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimetion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject donoGameObject = null;       //ゲームオブジェクト保管庫 ※操作するものがそのオブジェクトなら※1に
                                                             //(インスペクター内でいじれるように[SerializeField]を最初に入れる)
    RectTransform donoTransForm = null;                     //UI内のTransformを変更するのに使うやつ
    Vector3 donoPosition = Vector3.zero;                    //位置座標を変更するのに使う
    [SerializeField] float Speed = 0.0f;
    //位置を変更するのに使うのであれば

    float FlameCount = 0.0f;

    void Awake()
    {
        //donoTransForm = this.GetComponent<RectTransform>(); //※1 UIにスクリプトをアタッチして、そのUIを動かすならこっち
        donoTransForm = donoGameObject.GetComponent<RectTransform>();       //ゲームオブジェクトのUIのtransformを取得
        donoPosition = donoTransForm.localPosition;                         //位置座標を取得
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FlameCount += 0.1f;

        

        donoTransForm.rotation = Quaternion.Euler(0, 0, 8 * Mathf.Sin(FlameCount) );
        
        if(FlameCount > 180.0f)
            FlameCount = 0.0f;

        //変更した位置をRectTransformに格納する
        donoTransForm.localPosition = donoPosition;
    }
}