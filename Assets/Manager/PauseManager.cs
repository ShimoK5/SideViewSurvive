using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    enum PAUSE_CHOOSE
    {
        SCREEN_SET,
        BACK_TITLE,
        CLOSE_PAUUSE,
        PAUSE_STATE_MAX
    }

    enum PAUSE_STATE
    {
        START,
        PAUSE,
        END,
    }



    int PauseChoose = (int)PAUSE_CHOOSE.SCREEN_SET;     //選択中のPouse
    PAUSE_STATE PauseState = PAUSE_STATE.START;         //ポーズの状態
    [Header("ポーズアニメ待ち時間(フレーム)")]
    [SerializeField] int AnimWaitFlame;
    int PauseStateCount;        //ポーズ画面でのカウント

    float PauseBackGroundWidth;

    public static PauseManager instanse;
    public GameObject PauseCanvas;      //ポーズキャンバス
    public GameObject PauseBackGround;  //ポーズ背景
    public GameObject[] ButtonArray = new  GameObject [(int)PAUSE_CHOOSE.PAUSE_STATE_MAX];//ボタン
    public GameObject Flame;    //枠
   

    // Start is called before the first frame update
    void Awake()
    {
        instanse = this;
    }

    void Start()
    {
        PauseBackGroundWidth = PauseBackGround.GetComponent<RectTransform>().sizeDelta.x;
        PauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                //Pause入力あれば
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameStateManager.instance.GameState = GAME_STATE.Pause;
                    PauseState = PAUSE_STATE.START;
                    PauseChoose = 0;
                }
                break;
            case GAME_STATE.Pause:
                //Pause入力あれば
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //GameStateManager.instance.GameState = GAME_STATE.Game;
                    PauseState = PAUSE_STATE.END;
                    
                }
                break;
            default:
                break;
        }



        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                Time.timeScale = 1f;
                FixedGame();
                break;
            case GAME_STATE.Pause:
                Time.timeScale = 0f;
                FixedPause();
                break;
            default:
                break;
        }

        void FixedGame()
        {
            //ポーズ画面非表示
            PauseCanvas.SetActive(false);
        }

        void FixedPause()
        {
            switch (PauseState)
            {
                case PAUSE_STATE.START:
                    FixedPauseStart();
                    break;
                case PAUSE_STATE.PAUSE:
                    FixedPausePause();
                    break;
                case PAUSE_STATE.END:
                    FixedPauseEnd();
                    break;

            }
        }
    }

    void FixedPauseStart()
    {
        //カーソル値変化
        MoveCursor();

        PauseStateCount++;
        //ポーズ画面表示
        PauseCanvas.SetActive(true);

        SetParm((float)(PauseStateCount) / AnimWaitFlame);

        if (PauseStateCount >= AnimWaitFlame)
        {
            PauseStateCount = 0;
            PauseState = PAUSE_STATE.PAUSE;
        }
    }

    void FixedPausePause()
    {
        //カーソル値変化
        MoveCursor();

        //処理を行う場合は処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (PauseChoose)
            {
                case (int)PAUSE_CHOOSE.SCREEN_SET:
                    break;

                case (int)PAUSE_CHOOSE.BACK_TITLE:
                    break;

                case (int)PAUSE_CHOOSE.CLOSE_PAUUSE:
                    PauseState = PAUSE_STATE.END;
                    break;

            }
        }
    }
    
    void FixedPauseEnd()
    {
        //カーソル値変化
        MoveCursor();

        PauseStateCount++;

        SetParm((float)(AnimWaitFlame - PauseStateCount) / AnimWaitFlame);

        if (PauseStateCount >= AnimWaitFlame)
        {
            PauseStateCount = 0;
            PauseState = PAUSE_STATE.START;
            GameStateManager.instance.GameState = GAME_STATE.Game;
        }
    }

    void MoveCursor()
    {
        //カーソル値変化
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PauseChoose++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PauseChoose--;
        }

        PauseChoose = (PauseChoose + (int)PAUSE_CHOOSE.PAUSE_STATE_MAX) % (int)PAUSE_CHOOSE.PAUSE_STATE_MAX;

        //カーソル移動
        Flame.transform.position = ButtonArray[PauseChoose].transform.position;
    }

    //キャンバス内のもののアルファ値を変更
    void SetParm(float Parm)
    {
        //PauseBackGround.GetComponent<Image>().color = new Color(1, 1, 1, Parm);
        PauseBackGround.GetComponent<RectTransform>().sizeDelta = new Vector2 (PauseBackGroundWidth * Parm ,
            PauseBackGround.GetComponent<RectTransform>().sizeDelta.y);
        for (int i = 0; i < ButtonArray.Length; i++)
        {
            ButtonArray[i].GetComponent<Image>().color = new Color(1, 1, 1, Parm);
        }
        Flame.GetComponent<Image>().color = new Color(1, 1, 1, Parm);
    }
}
