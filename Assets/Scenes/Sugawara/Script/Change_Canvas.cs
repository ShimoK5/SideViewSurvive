using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Canvas : MonoBehaviour
{
    bool ChangeFlag = false;
    GameObject Canvas;
    GameObject MapCanvas;
    bool ButtonFlag = false;
    [SerializeField] GameObject[] GuideUI = new GameObject[2];
   
    // Start is called before the first frame update
    void Awake()
    {
        ChangeFlag = false;
        Canvas = GameObject.Find("Canvas");
        MapCanvas = GameObject.Find("MapCanvas");
        MapCanvas.SetActive(false);
        GuideUI[1].SetActive(false);
        ButtonFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ButtonFlag == false &&CustomTimeLine.instance.GetStopTimeLine() == true)
        {
            ButtonFlag = true;
        }

        if (ButtonFlag == true)
        {
            if (SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.X_BUTTON))
            {


                if (ChangeFlag == false)
                {
                    MapCanvas.SetActive(true);
                    GuideUI[1].SetActive(true);
                    MapMetronome.instance.SetUI();
                    GuideUI[0].SetActive(false); 
                    Canvas.SetActive(false);                  
                    ChangeFlag = true;
                }
                else
                {
                    Canvas.SetActive(true);
                    GuideUI[0].SetActive(true);
                    MapCanvas.SetActive(false);
                    GuideUI[1].SetActive(false);
                    ChangeFlag = false;
                }
            }
        }
    }
}
