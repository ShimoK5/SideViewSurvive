using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    enum PAUSE_STATE
    {
        SCREEN_SET,
        BACK_TITLE,
        CLOSE_PAUUSE,
        PAUSE_STATE_MAX
    }

    int SelectState = (int)PAUSE_STATE.SCREEN_SET;

    public static PauseManager instanse;
    public GameObject PauseCanvas;  //ポーズ背景
    public GameObject[] ButtonArray = new  GameObject [(int)PAUSE_STATE.PAUSE_STATE_MAX];//ボタン
    public GameObject Flame;    //枠
    bool OldIsPause;
    public bool isPause;

    // Start is called before the first frame update
    void Awake()
    {
        instanse = this;
    }

    void Start()
    {
        isPause = OldIsPause = false;
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
                }
                break;
            case GAME_STATE.Pause:
                //Pause入力あれば
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameStateManager.instance.GameState = GAME_STATE.Game;
                    SelectState = 0;
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
            //ポーズ画面表示
            PauseCanvas.SetActive(true);

            //カーソル値変化
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectState++;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectState--;
            }

            SelectState = (SelectState + (int)PAUSE_STATE.PAUSE_STATE_MAX) % (int)PAUSE_STATE.PAUSE_STATE_MAX;

            //カーソル移動
            Flame.transform.position = ButtonArray[SelectState].transform.position;


            //処理を行う場合は処理
            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (SelectState)
                {
                    case (int)PAUSE_STATE.SCREEN_SET:
                        break;

                    case (int)PAUSE_STATE.BACK_TITLE:
                        break;

                    case (int)PAUSE_STATE.CLOSE_PAUUSE:
                        isPause = false;
                        break;

                }
            }
        }
    }
}
