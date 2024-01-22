using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Canvas : MonoBehaviour
{
    bool ChangeFlag = false;
    GameObject Canvas;
    GameObject MapCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        ChangeFlag = false;
        Canvas = GameObject.Find("Canvas");
        MapCanvas = GameObject.Find("MapCanvas");
        MapCanvas.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.X_BUTTON))
        {
            if(ChangeFlag == false)
            {
                Canvas.SetActive(false);
                MapCanvas.SetActive(true);
                ChangeFlag = true;
            }
            else
            {
                Canvas.SetActive(true);
                MapCanvas.SetActive(false);
                ChangeFlag = false;
            }
        }
    }
}
