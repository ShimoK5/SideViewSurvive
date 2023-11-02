using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MoveHand : MonoBehaviour
{
    [SerializeField] private Vector3 HandPosition = new Vector3(0.0f, 0.0f, 0.0f);       //ハンドの初期位置
    [SerializeField] private float MoveSpeed = 0.1f;                                        //移動スピード
    //[SerializeField] private RectTransform Icon_Transform = null;                           //アイコンの位置、話したときに戻す用
    [SerializeField] private bool DragJudge = false;                                        //アイコンを掴めるかどうかの確認
    [SerializeField] private bool DragAndDrop = false;                                      //アイコン掴んでいるかどうか 
    [SerializeField] public GameObject Catch_Icon = null;                                  //アイコン保管用（多分消せる）
    [SerializeField] private Vector3 Catch_IconPosition = Vector3.zero;                     //移動する際に使用する位置座標（後で変数にして削除予定）
    public static MoveHand instance;                                                        //ノートボックスを格納するためのインスタンス化
    [SerializeField] public GameObject[] NoteBox = new GameObject[8];                       //ノートボックスを格納する用
    [SerializeField] int FlameCount = 0;                                                    //フレーム確認
    [SerializeField] int NoteNum = 0;                                                       //ノート数確認用
    [SerializeField] bool NoteCollision = false;                                            //ノートにぶつかっているか確認用
    [SerializeField] private MovieChange Movie = null;                                      //ムービー変化する用
    [SerializeField] bool ChangeScene = false;                                              //シーンチェンジ用

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;                                                                    //インスタンス化
    }

    void Start()
    {
        HandPosition = new Vector3(0.0f, 0.0f, 0.0f);
        RectTransform HandTransform = this.GetComponent<RectTransform>();
        HandTransform.TransformPoint(HandPosition);
        DragAndDrop = false;
        FlameCount = 0;
        NoteNum = 0;
        NoteCollision = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (DragAndDrop == false && collision.gameObject.tag == "Icon")
        {
            DragJudge = true;
            Catch_Icon = collision.gameObject;
            ChangeMovie(Catch_Icon.name);
            Catch_IconPosition = Catch_Icon.GetComponent<RectTransform>().localPosition;
            Catch_Icon.GetComponent<Image>().color = Color.red;
            //if (FlameCount % 5 == 0)
            //{
            //    NoteBox[NoteNum].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            //}
            //else
            //{
            //    NoteBox[NoteNum].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //}     いらんかもー

        }
        else if (DragAndDrop == false && collision.gameObject.name == "NextStage")
        {
            ChangeScene = true;
            collision.gameObject.GetComponent<Text>().color = Color.red;
           
        }
        else if(collision.gameObject.tag == "Note")
        {
            NoteCollision = true;
            for(int i = 0; i < 8; i ++)
            {
                if(collision.gameObject.name == NoteBox[i].name)
                {
                    NoteNum = i;
                    NoteBox[i].GetComponent<Image>().color = Color.red;
                    //Debug.Log(collision.GetComponent<Image>().sprite.name);
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
            //if (FlameCount % 5 == 0)
            //{
            //    collision.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            //}
            //else
            //{
            //    collision.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //}　　　いらんかもー

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
        if(collision.gameObject.name == "NextStage")
        {
            collision.gameObject.GetComponent<Text>().color = Color.black;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (ChangeScene == true && (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene("ShimokawaraScene 1");
            RhythmManager.Instance.FCnt = 0;
        }


        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
            DoCarryIcon();
        }
        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            DoCarryIcon();
            MoveChange();
            RectTransform HandTransform = this.GetComponent<RectTransform>();
            HandTransform.localPosition = HandPosition;
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


        

    }

    void MoveChange()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            HandPosition.x = HandPosition.x + MoveSpeed;
            if (HandPosition.x > 400.0f)
            {
                HandPosition.x = 400.0f;
            }
            if (DragAndDrop == true)
            {
                Catch_IconPosition.x += MoveSpeed;
                
            }

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            HandPosition.x = HandPosition.x + MoveSpeed * -1.0f;
            if (HandPosition.x < -400.0f)
            {
                HandPosition.x = -400.0f;
            }

            if (DragAndDrop == true)
            {
                Catch_IconPosition.x += MoveSpeed * -1.0f;
               
            }
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            HandPosition.y = HandPosition.y + MoveSpeed;
            if (HandPosition.y > 320.0f)
            {
                HandPosition.y = 320.0f;
            }

            if (DragAndDrop == true)
            {
                Catch_IconPosition.y += MoveSpeed;
               
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            HandPosition.y = HandPosition.y + MoveSpeed * -1.0f;
            if (HandPosition.y < -320.0f)
            {
                HandPosition.y = -320.0f;
            }
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
            if (DragJudge == true &&(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Space)))
            {
                DragAndDrop = true;
                GameObject CloneIconBox = null;
                CloneIconBox = Instantiate(Catch_Icon, Catch_IconPosition, Quaternion.identity);
                CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
                CloneIconBox.transform.SetSiblingIndex(3);
                CloneIconBox.name = Catch_Icon.name;
            }
            else if (NoteCollision == true &&(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Backspace)) )
            {
                NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.None;
            }
        }
        else if (DragAndDrop == true)
        {
            if (NoteCollision == true &&(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Space)) )
            {
                switch (Catch_Icon.GetComponent<Image>().sprite.name)
                {
                    case ("Umbrella_Waku"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Umbrella;
                        break;

                    case ("Quaver_Waku"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Recorder;
                        break;

                    case ("Eraser_Waku"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Eraser;
                        break;

                    case ("Scrifice_Waku"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Sacrifice;
                        break;

                    case ("AirCannon_Waku"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.AirCannon;
                        break;

                    case ("Bag_Waku"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Bag;
                        break;

                    case ("Ruler_Waku"):
                        NoteBox[NoteNum].GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                        RhythmManager.Instance.ActionArray[NoteNum] = RhythmManager.RhythmAction.Ruler;
                        break;

                    case ("Whistle_Waku"):
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
            else if ((Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Space)) && NoteCollision == false)
            {
                DragAndDrop = false;
                Destroy(Catch_Icon);
            }
            else if ((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Backspace)) && NoteCollision == true)
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