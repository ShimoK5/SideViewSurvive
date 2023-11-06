using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    float PauseBackGroundWidth; //通常の背景の横幅

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
        if(SceneChangeManager.instance.isFade)
        {
            return;
        }

        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                //Pause入力あれば
                if (InputManager_U.instanse.GetKeyTrigger(Key.Start))
                {
                    //ゲームステート変更
                    GameStateManager.instance.GameState = GAME_STATE.Pause;
                    //タイムスケール変更
                    Time.timeScale = 0f;
                    //ポーズステート変更
                    PauseState = PAUSE_STATE.START;
                    //選択物初期化
                    PauseChoose = 0;
                }
                break;
            case GAME_STATE.Pause:
                //Pause入力あれば
                if (InputManager_U.instanse.GetKeyTrigger(Key.Start) ||
                    InputManager_U.instanse.GetKeyTrigger(Key.A))
                {
                    //GameStateManager.instance.GameState = GAME_STATE.Game;
                    //ポーズステート変更
                    PauseState = PAUSE_STATE.END;
                    
                }
                break;
            default:
                break;
        }



        switch (GameStateManager.instance.GameState)
        {
            case GAME_STATE.Game:
                
                FixedGame();
                break;
            case GAME_STATE.Pause:
                
                FixedPause();
                break;
            default:
                break;
        }

        
    }
    void FixedGame()
    {
        Time.timeScale = 1f;
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
        if (InputManager_U.instanse.GetKeyTrigger(Key.B))
        {
            switch (PauseChoose)
            {
                case (int)PAUSE_CHOOSE.SCREEN_SET:
                    Time.timeScale = 1.0f;
                    //SceneManager.LoadScene("SetScene");
                    SceneChangeManager.instance.SceneTransition("SetScene",2,SceneChangeManager.TransitionType.Black);
                    break;

                case (int)PAUSE_CHOOSE.BACK_TITLE:
                    Time.timeScale = 1.0f;
                    SceneChangeManager.instance.SceneTransition("Title");
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
        if (InputManager_U.instanse.LongPushCoolInput(Key.Down))
        {
            PauseChoose++;
        }
        else if (InputManager_U.instanse.LongPushCoolInput(Key.Up))
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
