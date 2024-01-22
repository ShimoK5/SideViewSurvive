using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MapCursorAction : MonoBehaviour
{
    MapAction Action;                                                    //マップ内の行動を参考する用
    Image RightCursor;                                                  //右カーソルの画像
    Image LeftCursor;                                                   //左カーソルの画像

    [SerializeField] Sprite OffCursor;                                  //押されてないときのカーソル画像
    [SerializeField] Sprite OnCursor;                                   //押されているときのカーソル画像

    Vector3 RightCursorInitPosition = new Vector3(0.0f,0.0f,0.0f);      //右カーソル初期位置
    Vector3 LeftCursorInitPosition = new Vector3(0.0f, 0.0f, 0.0f);     //左カーソル初期位置
    Vector3 MovePosition = new Vector3(50.0f, 0.0f, 0.0f);              //移動位置

    void Awake()
    {
        if(Action == null)
        {
            Action = GameObject.Find("MapFrame").GetComponent<MapAction>();
            RightCursor = GameObject.Find("Right_Cursor").GetComponent<Image>();
            LeftCursor = GameObject.Find("Left_Cursor").GetComponent<Image>();
        }
        RightCursorInitPosition = RightCursor.transform.localPosition;
        LeftCursorInitPosition = LeftCursor.transform.localPosition;
        MovePosition = new Vector3(50.0f, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        MapAction.STATE MapState = Action.GetState();

        if(MapState == MapAction.STATE.Right)
        {
            //画像変更
            RightCursor.sprite = OnCursor;
            LeftCursor.sprite = OffCursor;
            //位置変更
            RightCursor.transform.localPosition = RightCursorInitPosition + MovePosition;
            LeftCursor.transform.localPosition = LeftCursorInitPosition;
        }
        else if (MapState == MapAction.STATE.Left)
        {
            //画像変更
            RightCursor.sprite = OffCursor;
            LeftCursor.sprite = OnCursor;
            //位置変更
            RightCursor.transform.localPosition = RightCursorInitPosition;
            LeftCursor.transform.localPosition = LeftCursorInitPosition - MovePosition;
        }
        else if(MapState == MapAction.STATE.Idle)
        {
            //画像変更
            RightCursor.sprite = OffCursor;
            LeftCursor.sprite = OffCursor;
            //位置変更
            RightCursor.transform.localPosition = RightCursorInitPosition;
            LeftCursor.transform.localPosition = LeftCursorInitPosition;
        }
    }
}
