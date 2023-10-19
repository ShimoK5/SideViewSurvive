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
    public GameObject PauseCanvas;
    public GameObject[] ButtonArray = new  GameObject [(int)PAUSE_STATE.PAUSE_STATE_MAX];
    public GameObject Flame;
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
        OldIsPause = isPause;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //フラグ反転
            isPause = !isPause;
        }
        
        
        if(isPause)
        {
            PauseCanvas.SetActive(true);
            Time.timeScale = 0f;
       
            if(!OldIsPause)
            {
                SelectState = 0;
            }
        }
        else
        {
            PauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        //ポーズ中なら
        if(isPause)
        {
            //カーソル値変化
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectState++;
            }
            else if(Input. GetKeyDown(KeyCode.UpArrow))
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
