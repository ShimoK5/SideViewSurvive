using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyButton : MonoBehaviour
{
    RectTransform rtf;      //レクトトランスフォーム
    Image img;              //イメージ


    int NormalCnt = 0;
    int WaitCnt = 0;

    Vector3 DefaultPos;
    Vector3 DefaultScale;
    Color   DefaultColor;

    Vector3 AnotherPos;
    Vector3 AnotherScale;
    Color   AnotherColor;

    float WaitAlpha = 1.0f;
    static float AlphaMoveRatio = 0.33f;

    // Start is called before the first frame update
    void Start()
    {
        NormalCnt = 0;
        WaitCnt = 0;

        rtf = GetComponent<RectTransform>();
        img = GetComponent<Image>();

        DefaultPos = rtf.localPosition;
        DefaultScale = rtf.localScale;
        DefaultColor = img.color;
        AnotherPos = DefaultPos + new Vector3(0, -20, 0);
        AnotherScale = DefaultScale * 0.9f;
        AnotherColor.r = DefaultColor.r * 0.6f;        AnotherColor.g = DefaultColor.g * 0.6f;        AnotherColor.b = DefaultColor.b * 0.6f;
        AnotherColor.a = 1.0f;

        WaitAlpha = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (TitleStateManager.instans.state)
        {
            case TitleStateManager.State.Normal:
                NormalFixed();
                break;
            case TitleStateManager.State.After:
                AfterFixed();
                break;

        }

    }

    void NormalFixed()
    {
        NormalCnt++;

        if((NormalCnt / 30) % 2 == 0)
        {
            rtf.localPosition = DefaultPos;
            rtf.localScale = DefaultScale;
            img.color = DefaultColor;
        }
        else
        {
            rtf.localPosition = AnotherPos;
            rtf.localScale = AnotherScale;
            img.color = AnotherColor;
        }
    }

    void AfterFixed()
    {
        WaitCnt++;

        rtf.localPosition = DefaultPos;
        rtf.localScale = DefaultScale;

        if (WaitCnt >= TitleStateManager.instans.WAIT_MAX_CNT - 30)
        {
            WaitAlpha = Mathf.Min(WaitAlpha + AlphaMoveRatio, 1.0f);

            
            img.color = new Color(DefaultColor.r, DefaultColor.g, DefaultColor.b , WaitAlpha);
        }

        else
        {
            if ((WaitCnt / 5) % 2 == 0)
            {
                WaitAlpha = Mathf.Max(WaitAlpha - AlphaMoveRatio, 0.0f);
                img.color = new Color(DefaultColor.r, DefaultColor.g, DefaultColor.b, WaitAlpha);
            }
            else
            {
                WaitAlpha = Mathf.Min(WaitAlpha + AlphaMoveRatio, 1.0f);
                img.color = new Color(DefaultColor.r, DefaultColor.g, DefaultColor.b, WaitAlpha);
            }

        }


    }

}
