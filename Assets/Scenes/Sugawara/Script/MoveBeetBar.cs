using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeetBar : MonoBehaviour
{
    [SerializeField] public RhythmManager Manager = null;                   //リズムマネージャー変数の取得用
    [SerializeField] public int Tempo = 0;                                  //テンポ変数の確認用
    [SerializeField] RectTransform BarTransForm = null;                     //UIの位置を変更する際に必要なUI用のトランスフォーム変数
    private float Init_BarPosition_x = 0.0f;                             //初期位置の移動位置
    private Vector3 BarPosition = new Vector3(0.0f, 0.0f, 0.0f);      //移動するバーの位置情報
    private float Move_BarSpeed = 0.0f;                                     //移動スピードの変数
    private bool FirstCount = true;

    // Start is called before the first frame update
    void Start()
    {
        BarTransForm = this.GetComponent<RectTransform>();                  //このスクリプトについているRectTransformを取得
        Init_BarPosition_x = BarTransForm.localPosition.x;
        BarPosition = new Vector3(BarTransForm.localPosition.x, BarTransForm.localPosition.y,0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Manager == null)
        {
            GameObject rhythm = GameObject.Find("RhythmManager");
            Manager = rhythm.GetComponent<RhythmManager>();
            FirstCount = true;
            Move_BarSpeed = (270.0f) / (Manager.BeatTempo * 8);
        }

        Tempo = Manager.FCnt;

        if (FirstCount == true && Tempo <= Manager.BeatTempo)           //初回時で１回目のサイクルフレーム未満の場合
        {
            BarPosition.x = Init_BarPosition_x + Move_BarSpeed * (Tempo - Manager.BeatTempo);
            BarTransForm.localPosition = BarPosition;
        }
        else if (FirstCount == true && Tempo >= Manager.BeatTempo)      //初回時で2～7サイクルフレームの時
        {
            FirstCount = false;
            BarPosition.x = Init_BarPosition_x + Move_BarSpeed * (Tempo - Manager.BeatTempo);
            BarTransForm.localPosition = BarPosition;
        }
        else if(FirstCount == false && Tempo <= Manager.BeatTempo)      //初回以外で8サイクル時の時
        {
            BarPosition.x = Init_BarPosition_x + Move_BarSpeed * (Tempo + Manager.BeatTempo * 7.0f);
            BarTransForm.localPosition = BarPosition;
        }
        else                                                            //その他のサイクル時
        {
            BarPosition.x = Init_BarPosition_x + Move_BarSpeed * (Tempo - Manager.BeatTempo);
            BarTransForm.localPosition = BarPosition;
        }

    }
}
