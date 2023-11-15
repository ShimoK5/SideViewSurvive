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
    [SerializeField] private bool TouchJudge = false;                                       //触っているかどうか
    [SerializeField] private bool DragAndDrop = false;                                      //アイコン掴んでいるかどうか
    [SerializeField] private GameObject Touch_Object = null;                                //触ったオブジェクト
    [SerializeField] private GameObject Duplication_Object = null;                          //オブジェクトが重なってしまった場合
    [SerializeField] private GameObject DragAndDropObject = null;
    [SerializeField] private Vector3 Catch_IconPosition = Vector3.zero;                     //移動する際に使用する位置座標（後で変数にして削除予定）
    [SerializeField] int NoteNum = 0;                                                       //ノート数確認用
    [SerializeField] bool NoteCollision = false;                                            //ノートにぶつかっているか確認用
    [SerializeField] bool ChangeScene = false;                                              //シーンチェンジ用
    [SerializeField] int ChangeMovieFlame = 0;                                         //シーンの変化に対応したフレーム数
    [SerializeField] bool MovieNoise = false;
    [SerializeField] GameObject MovieObject;
    [SerializeField] private MovieChange Movie = null;                                      //ムービー変化する用

    [SerializeField] public GameObject[] NoteBox = new GameObject[8];                       //ノートボックスを格納する用
    [SerializeField] Material Ma;

    [SerializeField] private bool DragJudge = false;                                        //アイコンを掴めるかどうかの確認
    [SerializeField] public GameObject Catch_Icon = null;                                  //アイコン保管用（多分消せる）

    // Start is called before the first frame update

    void Start()
    {
        HandPosition = new Vector3(0.0f, 0.0f, 0.0f);
        RectTransform HandTransform = this.GetComponent<RectTransform>();
        HandTransform.TransformPoint(HandPosition);
        DragAndDrop = false;
        NoteNum = 0;
        NoteCollision = false;
        Movie = MovieObject.GetComponent<MovieChange>();
        //DragAndDropObject = AssetManager.Instance.PrefabObject[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Note" || collision.gameObject.name == "NextStage")
        {
            if (TouchJudge == false)
            {
                TouchJudge = true;
                Touch_Object = collision.gameObject;
            }
            else
            {
                Duplication_Object = collision.gameObject;
            }
            
        }
        else if(collision.gameObject.name == "Target")
        {
            TouchJudge = true;
            Touch_Object = collision.gameObject.transform.parent.gameObject;
            collision.GetComponent<TargetCollision>().TouchImage();
            ChangeMovie(Touch_Object.name);
        }
        else if (DragAndDrop == false && collision.gameObject.name == "NextStage")
        {
            collision.gameObject.GetComponent<Text>().color = Color.red;           
        }
        else
        {
            DragJudge = false;
            ChangeScene = false;
            
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
      
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Note" || collision.gameObject.name == "NextStage")
        {
            if (Duplication_Object == null)
            {
                TouchJudge = false;
                Touch_Object = null;
            }
            else if(Duplication_Object != null)
            {
                TouchJudge = true;
                Touch_Object = Duplication_Object;
                Duplication_Object = null;
            }

            if (collision.gameObject.name == "NextStage")
            {
                collision.gameObject.GetComponent<Text>().color = Color.black;
            }
        }
        else if (collision.gameObject.name == "Target")
        {
            TouchJudge = false;
            collision.GetComponent<TargetCollision>().DontTouchImage();
            Touch_Object = null;
        }

        
    }


    // Update is called once per frame
    void Update()
    {
        if(TouchJudge == true && (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Space)))
        {
            TouchDoPush();
        }
        else if (TouchJudge == false && (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Space)))
        {
            DoAButtonPush();
        }

        


        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
           // DoCarryIcon();
        }
        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            //DoCarryIcon();
            MoveChange();
            RectTransform HandTransform = this.GetComponent<RectTransform>();
            HandTransform.localPosition = HandPosition;
            if (DragAndDrop == true)
            {
                DragAndDropObject.GetComponent<RectTransform>().localPosition = HandPosition;
            }           
        }

        if(MovieNoise == true)
        {
            Ma = MovieObject.GetComponent<RawImage>().material;
            //var material = GetComponent<Renderer>().material;
            Ma.SetFloat("_BoolSwitch",1.0f);
            Ma.EnableKeyword("FILL_WITH_RED");
            ChangeMovieFlame += 1;
            if (ChangeMovieFlame > 180)
            {
                Ma.SetFloat("_BoolSwitch", 0.0f);
                ChangeMovieFlame = 0;
                MovieNoise = false;
                Ma.DisableKeyword("FILL_WITH_RED");
            }
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

    void TouchDoPush()
    {
        if (Touch_Object.tag == "Icon")
        {
            if (DragAndDrop == false)
            {
                CreateCloneIcon();
                DragAndDrop = true;
            }
            else
            {
                DragAndDrop = false;
                 Destroy(DragAndDropObject);
            }
        }
        else if(Touch_Object.tag == "Note")
        {
            if (DragAndDrop == true)
            {
                switch (DragAndDropObject.name)
                {
                    case ("Umbrella"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.Umbrella, Touch_Object.name);
                        break;

                    case ("Recorder"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.Recorder, Touch_Object.name);
                        break;

                    case ("Eraser"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.Eraser, Touch_Object.name);
                        break;

                    case ("Sacrifice"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.Sacrifice, Touch_Object.name);
                        break;

                    case ("AirCannon"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.AirCannon, Touch_Object.name);
                        break;

                    case ("Bag"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.Bag, Touch_Object.name);
                        break;

                    case ("Ruler"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.Ruler, Touch_Object.name);
                        break;

                    case ("Whistle"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.Whistle, Touch_Object.name);
                        break;

                    case ("None"):
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.None, Touch_Object.name);
                        break;

                    default:
                        InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.None, Touch_Object.name);
                        break;

                }
            }
            else
            {
                InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.None, Touch_Object.name);
            }
        }
        else if (Touch_Object.name == "NextStage")
        {
            if (DragAndDrop == false)
            {
                SceneChangeManager.instance.SceneTransition("ShimokawaraScene 1");
                RhythmManager.Instance.FCnt = 0;
            }
            else
            {
                DragAndDrop = false;
                Destroy(DragAndDropObject);
            }
        }
    }

    void DoAButtonPush()
    {
        if(DragAndDrop == true)
        {
            DragAndDrop = false;
            Destroy(DragAndDropObject);
        }
    }
    
    void CreateCloneIcon()
    {
    
        GameObject CloneIconBox = null;
        CloneIconBox = Instantiate(AssetManager.Instance.PrefabObject[0], HandPosition, Quaternion.identity);
        CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
        CloneIconBox.transform.SetSiblingIndex(3);
        CloneIconBox.name = Touch_Object.name;
        switch (CloneIconBox.name)
        {
            case ("Umbrella"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Umbrella);
                break;

            case ("Recorder"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Recorder);
                break;

            case ("Eraser"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Eraser);
                break;

            case ("Sacrifice"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Sacrifice);
                break;

            case ("AirCannon"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.AirCannon);
                break;

            case ("Bag"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Bag);
                break;

            case ("Ruler"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Ruler);
                break;

            case ("Whistle"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.Whistle);
                break;

            case ("None"):
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                break;

            default:
                CloneIconBox.GetComponent<Image>().sprite = AssetManager.Instance.ReferenceSpriteBox(AssetManager.ActionName.None);
                break;

        }
        DragAndDropObject = CloneIconBox;
    }




    void ChangeMovie(string Name)
    {
        if (MovieNoise == false)
        {
            MovieNoise = true;
        }
        ChangeMovieFlame = 0;
        switch (Name)
        {
            case ("Umbrella"):
                Movie.Change(RhythmManager.RhythmAction.Umbrella);
                break;

            case ("Recorder"):
                Movie.Change(RhythmManager.RhythmAction.Recorder);
                break;

            case ("Eraser"):
                Movie.Change(RhythmManager.RhythmAction.Eraser);
                break;

            case ("Sacrifice"):
                Movie.Change(RhythmManager.RhythmAction.Sacrifice);
                break;

            case ("AirCannon"):
                Movie.Change(RhythmManager.RhythmAction.AirCannon);
                break;

            case ("Bag"):
                Movie.Change(RhythmManager.RhythmAction.Bag);
                break;

            case ("Ruler"):
                Movie.Change(RhythmManager.RhythmAction.Ruler);
                break;

            case ("Whistle"):
                Movie.Change(RhythmManager.RhythmAction.Whistle);
                break;

            default:
                break;
        }
    }
}