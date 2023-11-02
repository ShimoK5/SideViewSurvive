using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MoveHand : MonoBehaviour
{
    [SerializeField] private Vector3 HandPosition = new Vector3(50.0f, -82.0f, 0.0f);       //ハンドの初期位置
    [SerializeField] private float MoveSpeed = 0.1f;                                        //移動スピード
    [SerializeField] private RectTransform Icon_Transform = null;                           //アイコンの位置、話したときに戻す用
    [SerializeField] private bool DragJudge = false;                                        //アイコンを掴めるかどうかの確認
    [SerializeField] private bool DragAndDrop = false;                                      //アイコン掴んでいるかどうか 
    [SerializeField] public GameObject Catch_Icon = null;                                  //アイコン保管用（多分消せる）
    [SerializeField] private Vector3 Catch_IconPosition = Vector3.zero;                     //移動する際に使用する位置座標（後で変数にして削除予定）
    public static MoveHand instance;                                                        //ノートボックスを格納するためのインスタンス化
    [SerializeField] public GameObject[] NoteBox = new GameObject[8];                       //ノートボックスを格納する用
    [SerializeField] int FlameCount = 0;                                                    //
    [SerializeField] int NoteNum = 0;
    [SerializeField] bool NoteCollision = false;
    [SerializeField] private MovieChange Movie = null;
    [SerializeField] bool ChangeScene = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        HandPosition = new Vector3(50.0f, -82.0f, 0.0f);
        Icon_Transform = this.GetComponent<RectTransform>();
        Icon_Transform.TransformPoint(HandPosition);
        DragAndDrop = false;
        FlameCount = 0;
        NoteNum = 0;
        NoteCollision = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnCollisionEnter2D: " + collision.gameObject.name);      //確認用　あとで削除予定
        if (DragAndDrop == false && collision.gameObject.tag == "Icon")
        {
            DragJudge = true;
            Catch_Icon = collision.gameObject;
            ChangeMovie(Catch_Icon.name);
            Catch_IconPosition = Catch_Icon.GetComponent<RectTransform>().localPosition;
            if (FlameCount % 5 == 0)
            {
                NoteBox[NoteNum].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            else
            {
                NoteBox[NoteNum].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }

        }
        else if (DragAndDrop == false && collision.gameObject.name == "NextStage")
        {
            ChangeScene = true;
           
        }
        else if(collision.gameObject.tag == "Note")
        {
            NoteCollision = true;
            for(int i = 0; i < 8; i ++)
            {
                if(collision.gameObject.name == NoteBox[i].name)
                {
                    NoteNum = i;
                    Debug.Log(collision.GetComponent<Image>().sprite.name);
                }
            }
        }
        else
        {
            DragJudge = false;
            ChangeScene = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (DragAndDrop == false && collision.gameObject.tag == "Icon" || collision.gameObject.tag == "Note")
        {
            if (FlameCount % 5 == 0)
            {
                collision.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            else
            {
                collision.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        DragJudge = false;
        ChangeScene = false;
        if (collision.gameObject.tag == "Icon")
        {
            collision.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        //SelectImage = null;
        if (collision.gameObject.tag == "Note")
        {
            NoteCollision = false;
            collision.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
            DoCarryIcon();
        }
        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            DoCarryIcon();
            MoveChange();
            //HandPosition = new Vector3(HandPosition.x + MoveSpeed * Input.GetAxis("Vertical"), HandPosition.y + MoveSpeed * Input.GetAxis("Horizontal"), 0.0f);
            Icon_Transform.localPosition = HandPosition;
            if (DragAndDrop == true)
            {
                Catch_Icon.GetComponent<RectTransform>().localPosition = Catch_IconPosition;
            }           
        }

        FlameCount += 1;
       
        if (FlameCount >=250)
        {
            FlameCount = 0;
        }


        if (ChangeScene == true && Input.GetKeyDown("joystick button 1"))
        {
            SceneManager.LoadScene("ShimokawaraScene 1");
            RhythmManager.Instance.FCnt = 0;
        }

    }

    void MoveChange()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            HandPosition.x = HandPosition.x + MoveSpeed;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.x += MoveSpeed;
            }

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            HandPosition.x = HandPosition.x + MoveSpeed * -1.0f;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.x += MoveSpeed * -1.0f;
            }
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            HandPosition.y = HandPosition.y + MoveSpeed;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.y += MoveSpeed;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            HandPosition.y = HandPosition.y + MoveSpeed * -1.0f;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.y += MoveSpeed * -1.0f;
            }
        }
    }

    void DoCarryIcon()
    {
        if (DragAndDrop == false)
        {
            if (Input.GetKeyDown("joystick button 1") && DragJudge == true)
            {
                DragAndDrop = true;
                GameObject CloneIconBox = null;
                CloneIconBox = Instantiate(Catch_Icon, Catch_IconPosition, Quaternion.identity);
                CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
                CloneIconBox.transform.SetSiblingIndex(3);
                CloneIconBox.name = Catch_Icon.name;
            }
            else if (Input.GetKeyDown("joystick button 0") && NoteCollision == true)
            {
                NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.None;
            }
        }
        else if (DragAndDrop == true)
        {
            if (Input.GetKeyDown("joystick button 1") && NoteCollision == true)
            {
                switch (Catch_Icon.GetComponent<Image>().sprite.name)
                {
                    case ("Umbrella"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Umbrella;
                        break;

                    case ("Quaver"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Recorder;
                        break;

                    case ("Eraser"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Eraser;
                        break;

                    case ("Sacrifice"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Sacrifice;
                        break;

                    case ("AirCannon"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.AirCannon;
                        break;

                    case ("Bag"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Bag;
                        break;

                    case ("Ruler"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Ruler;
                        break;

                    case ("Whistle"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Whistle;
                        break;

                    case ("None"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.None;
                        break;

                    default:
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.None;
                        break;

                }
            }
            else if (Input.GetKeyDown("joystick button 1") && NoteCollision == false)
            {
                DragAndDrop = false;
                Destroy(Catch_Icon);
            }
            else if (Input.GetKeyDown("joystick button 0") && NoteCollision == true)
            {
                NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.None;
            }
        }
    }

    void ChangeMovie(string Name)
    {
        switch(Name)
        {
            case ("0"):
                Movie.Change(RhythmManager.RhythmAction.Umbrella);
                break;

            case ("1"):
                Movie.Change(RhythmManager.RhythmAction.Recorder);
                break;

            case ("2"):
                Movie.Change(RhythmManager.RhythmAction.Eraser);
                break;

            case ("3"):
                Movie.Change(RhythmManager.RhythmAction.Sacrifice);
                break;

            case ("4"):
                Movie.Change(RhythmManager.RhythmAction.AirCannon);
                break;

            case ("5"):
                Movie.Change(RhythmManager.RhythmAction.Bag);
                break;

            case ("6"):
                Movie.Change(RhythmManager.RhythmAction.Ruler);
                break;

            case ("7"):
                Movie.Change(RhythmManager.RhythmAction.Whistle);
                break;

            default:
                break;
        }
    }
}