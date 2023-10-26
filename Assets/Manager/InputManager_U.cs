using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_U : MonoBehaviour
{
    //実体
    public static InputManager_U instanse;

    //入力保存配列
    bool[] InputArray = new bool[(int)Key.KeyMax];
    bool[] OldInputArray = new bool[(int)Key.KeyMax];

    //上下左右長押し時間保存配列
    int[] LongPushCount = new int[4];



    static float STICK_RATIO = 0.4f; //これ以上の入力値を受け入れる
    static int INPUT_FIRST_CNT = 7;     //長押ししたときに最初の入力を取得
    static int INPUT_COOL_CNT = 50;     //最初の長押し取得から何フレーム待つか
    static int INPUT_CYCLE_CNT = 20;    //長押し時のサイクルフレーム数



    void Awake()
    {
        instanse = this;
        //初期化
        for (int i = 0; i < InputArray.Length; i++)
        {
            InputArray[i] = false;
        }
        for (int i = 0; i < OldInputArray.Length; i++)
        {
            OldInputArray[i] = false;
        }
        for (int i = 0; i < LongPushCount.Length; i++)
        {
            LongPushCount[i] = 0;
        }


    }

    void Update()
    {

        for (int i = 0; i < (int)Key.KeyMax; i++)
        {
            //過去保存
            OldInputArray[i] = InputArray[i];
            //フラグリセット
            InputArray[i] = false;
        }

        //左スティック入力
        Vector2 LeftStick = Vector2.zero;
        LeftStick.x = Input.GetAxis("Horizontal");
        LeftStick.y = Input.GetAxis("Vertical");

        //十字キー入力
        Vector2 CrossKey = Vector2.zero;
        CrossKey.x = Input.GetAxis("JuujiKeyX");
        CrossKey.y = Input.GetAxis("JuujiKeyY");

        //右
        if (LeftStick.x > STICK_RATIO)//スティック
        {
            InputArray[(int)Key.Right] = true;
        }
        else if (CrossKey.x > STICK_RATIO)//十字キー
        {
            InputArray[(int)Key.Right] = true;
        }
        else if (Input.GetKey(KeyCode.D))//キーボード
        {
            InputArray[(int)Key.Right] = true;
        }

        //左
        if (!InputArray[(int)Key.Right])//右入力がなければ
        {
            if (LeftStick.x < -STICK_RATIO)//スティック
            {
                InputArray[(int)Key.Left] = true;
            }
            else if (CrossKey.x < -STICK_RATIO)//十字キー
            {
                InputArray[(int)Key.Left] = true;
            }
            else if (Input.GetKey(KeyCode.A))//キーボード
            {
                InputArray[(int)Key.Left] = true;
            }
        }

        //下
        if (LeftStick.y < -STICK_RATIO)//スティック
        {
            InputArray[(int)Key.Down] = true;
        }
        else if (CrossKey.y < -STICK_RATIO)//十字キー
        {
            InputArray[(int)Key.Down] = true;
        }
        else if (Input.GetKey(KeyCode.S))//キーボード
        {
            InputArray[(int)Key.Down] = true;
        }

        //上
        if (!InputArray[(int)Key.Down])//下入力がなければ
        {
            if (LeftStick.y > STICK_RATIO)//スティック
            {
                InputArray[(int)Key.Up] = true;
            }
            else if (CrossKey.y > STICK_RATIO)//十字キー
            {
                InputArray[(int)Key.Up] = true;
            }
            else if (Input.GetKey(KeyCode.W))//キーボード
            {
                InputArray[(int)Key.Up] = true;
            }
        }

        //Aボタン
        if (Input.GetKey("joystick button 0"))//コントローラー
        {
            InputArray[(int)Key.A] = true;
        }
        else if (Input.GetKey(KeyCode.Space))//キーボード
        {
            InputArray[(int)Key.A] = true;
        }

        //Bボタン
        if (Input.GetKey("joystick button 1"))//コントローラー
        {
            InputArray[(int)Key.B] = true;
        }
        else if (Input.GetKey(KeyCode.Return))//キーボード
        {
            InputArray[(int)Key.B] = true;
        }

        //Startボタン
        if (Input.GetKey("joystick button 7"))//コントローラー
        {
            InputArray[(int)Key.Start] = true;
        }
        else if (Input.GetKey(KeyCode.Escape))//キーボード
        {
            InputArray[(int)Key.Start] = true;
        }


        //長押しチェック
        for (int i = 0; i < LongPushCount.Length; i++)
        {
            if (InputArray[i])
            {
                //長押しカウント加算
                LongPushCount[i]++;
            }
            else
            {
                //長押しカウントリセット
                LongPushCount[i] = 0;
            }
        }
    }

    public bool GetKey(Key key)
    {
        return InputArray[(int)key];
    }

    public bool GetKeyTrigger(Key key)
    {
        //入力があり、前回は入力がない
        if (InputArray[(int)key] && !OldInputArray[(int)key])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //長押し時にちょうどいい周期でtrueが返る関数
    public bool LongPushCoolInput(Key key)
    {
        switch (key)
        {
            case Key.Right:
            case Key.Left:
            case Key.Up:
            case Key.Down:
                if (LongPushCount[(int)key] == INPUT_FIRST_CNT ||
                    (LongPushCount[(int)key] - INPUT_COOL_CNT) % INPUT_CYCLE_CNT == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;


            default:

                return false;
        }

    }
}
