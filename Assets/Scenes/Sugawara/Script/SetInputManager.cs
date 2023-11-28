using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInputManager : MonoBehaviour
{
    public enum BUTTON
    {
        A_BUTTON,           //Aボタン
        B_BUTTON,           //Bボタン
        X_BUTTON,           //Xボタン
        Y_BUTTON,           //Yボタン
        L1_BUTTON,          //L1ボタン
        R1_BUTTON,          //R1ボタン
        VIEW_BUTTON,        //switchの－ボタン・Xboxの四角いボタン
        MENU_BUTTON,        //switchの+ボタン・Xboxの三本線ボタン
        LEFT_STICK_BUTTON,  //左スティックボタン
        RIGHT_STICK_BUTTON, //右スティックボタン
        UP_BUTTON,          //↑ボタン
        DOWN_BUTTON,        //↓ボタン
        LEFT_BUTTON,        //←ボタン
        RIGHT_BUTTON,       //→ボタン              
        LEFT_STICK,         //左スティック
        RIGHT_STICK,        //右スティック
      
        MAX_BUTTON,
    }

    public static SetInputManager instance;

    //インスペクター内でいじれる用のキーコード設定
    [SerializeField] KeyCode A_Button;
    [SerializeField] KeyCode B_Button;
    [SerializeField] KeyCode X_Button;
    [SerializeField] KeyCode Y_Button;
    [SerializeField] KeyCode L1_Button;
    [SerializeField] KeyCode R1_Button;
    [SerializeField] KeyCode UP_Button;
    [SerializeField] KeyCode DOWN_Button;
    [SerializeField] KeyCode LEFT_Button;
    [SerializeField] KeyCode RIGHT_Button;
    [SerializeField] KeyCode View_Button;
    [SerializeField] KeyCode Menu_Button;

    [SerializeField] bool[] Push_Button = new bool[(int)BUTTON.MAX_BUTTON];         //現状の押したかの判定
    [SerializeField] bool[] OldPush_Button = new bool[(int)BUTTON.MAX_BUTTON];      //過去押したかの判定
    [SerializeField] float Left_Horizontal = 0.0f;                                  //左スティックの左右傾き加減
    [SerializeField] float Left_Vertical = 0.0f;                                    //左スティックの上下傾き加減


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        for (int i = 0; i < ((int)BUTTON.MAX_BUTTON); i++)
        {
            OldPush_Button[i] = false;
            Push_Button[i] = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        {
            Left_Horizontal = 0.0f;
            Left_Vertical = 0.0f;
            Left_Horizontal = Input.GetAxis("Horizontal");
            Left_Vertical = Input.GetAxis("Vertical");
        }

        for (int i = 0; i < ((int)BUTTON.MAX_BUTTON); i++)
        {
            OldPush_Button[i] = Push_Button[i];
            Push_Button[i] = false;
        }

        if(Input.GetKey("joystick button 0") || Input.GetKey(A_Button))
        {
            Push_Button[(int)BUTTON.A_BUTTON] = true;
            Debug.Log("A押し出来てます");
        }      

        if (Input.GetKey("joystick button 1") || Input.GetKey(B_Button))
        {
            Push_Button[(int)BUTTON.B_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 2") || Input.GetKey(X_Button))
        {
            Push_Button[(int)BUTTON.X_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 3") || Input.GetKey(Y_Button))
        {
            Push_Button[(int)BUTTON.Y_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 4") || Input.GetKey(L1_Button))
        {
            Push_Button[(int)BUTTON.L1_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 5") || Input.GetKey(R1_Button))
        {
            Push_Button[(int)BUTTON.R1_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 6") || Input.GetKey(View_Button))
        {
            Push_Button[(int)BUTTON.VIEW_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 7") || Input.GetKey(Menu_Button))
        {
            Push_Button[(int)BUTTON.MENU_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 8") || Input.GetKey(LEFT_Button))
        {
            Push_Button[(int)BUTTON.LEFT_STICK_BUTTON] = true;
        }

        if (Input.GetKey("joystick button 9") || Input.GetKey(RIGHT_Button))
        {
            Push_Button[(int)BUTTON.RIGHT_STICK_BUTTON] = true;
        }        

    }

    public bool Ref_Button(BUTTON key)
    {
        return Push_Button[(int)key];
    }

    public bool Ref_Trigger_Button(BUTTON key)
    {
        if (OldPush_Button[(int)key] == false && Push_Button[(int)key] == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Ref_LongPush_Button(BUTTON key)
    {
        if (Push_Button[(int)key] == true && OldPush_Button[(int)key] == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float Ref_Stick_Horizon(BUTTON key)
    {
        if (key == BUTTON.LEFT_STICK)
        {
            return Left_Horizontal;
        }
        else
        {
            Debug.Log("参照場所が違うよ");
            return 0.0f;
        }
    }

    public float Ref_Stick_Vertical(BUTTON key)
    {
        if (key == BUTTON.LEFT_STICK)
        {
            return Left_Vertical;
        }
        else
        {
            Debug.Log("参照場所が違うよ");
            return 0.0f;
        }
    }
}
