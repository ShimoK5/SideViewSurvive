using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MoveHand : MonoBehaviour
{
    private Vector3 IconShift = new Vector3(-40.0f, 0.0f, 0.0f);                            //アイコンの位置ずれ
    private Vector2 ShiftOffset = new Vector2(-30.0f, 0.0f);                                //当たり判定のずれ
    private Vector2 MaxMovePosition = new Vector2(650.0f, 100.0f);                          //移動上限値
    private Vector2 MinMovePosition = new Vector2(-600.0f, -900.0f);                        //移動下限値

    [SerializeField] private Vector3 HandPosition = Vector3.zero;                           //ハンドの初期位置
    [SerializeField] private float MoveSpeed = 10.0f;                                       //移動スピード
    [SerializeField] private int FreezeTime = 180;                                          //最初に動かせなくする用
    [SerializeField] private int FreezeCountTime = 0;                                       //セット画面が始まってからのカウント
    [SerializeField] private string touch_Name = null;                                      //触れたオブジェクトを判断するよう
    [SerializeField] private List<GameObject> Touch_Objects = new List<GameObject>();       //触ったオブジェクト達
    [SerializeField] private GameObject Touch_Object = null;
    [SerializeField] private GameObject Duplication_Object = null;                          //オブジェクトが重なってしまった場合
    [SerializeField] private GameObject DragAndDrop_Object = null;                          //掴んでいるオブジェクト
    [SerializeField] private bool TouchJudge = false;                                       //触っているかどうか
    [SerializeField] private bool DragAndDrop = false;                                      //アイコン掴んでいるかどうか
    [SerializeField] bool FistNoteChange = false;                                           //ノートにぶつかっているか確認用
    [SerializeField] bool Check = false;                                                    //シーンチェンジ用

    [SerializeField]bool CollisionCheck = false;
    bool ChangeTouch = false;

   

    // Start is called before the first frame update

    void Start()
    {
        HandPosition = new Vector3(0.0f, 0.0f, -10.0f);
        FreezeTime = 180;
        FreezeCountTime = 0;
        touch_Name = null;
        RectTransform HandTransform = this.GetComponent<RectTransform>();
        HandTransform.TransformPoint(HandPosition);
        DragAndDrop = false;
        FistNoteChange = false;
        Check = false;
        GameObject MovieObject = GameObject.Find("Movie");
        MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Whistle);
        ChangeFlavor("None");
        ChangeHeader("None");
        CollisionCheck = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Note" )
        {
            if (TouchJudge == false)
            {
                TouchJudge = true;
                Touch_Objects.Add(collision.gameObject);
                //Touch_Object.GetComponent<ChangeMetronome>().TouchMetronome();
            }
            else
            {
                Touch_Objects.Add(collision.gameObject);
            }            
        }       
        else if(collision.gameObject.tag == "Icon")
        {
            if (DragAndDrop == false)
            {
                touch_Name = collision.GetComponent<Change_Name_Item>().ItemName();
                ChangeFlavor(touch_Name);
                ChangeHeader(touch_Name);
                ChangeMovie(touch_Name);
                collision.GetComponent<Change_Name_Item>().Collision();
                TouchJudge = true;
            }
        }
        else if(collision.gameObject.tag == "PlatForm")
        {
            Touch_Object = collision.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (CollisionCheck == true)
        {
            if (collision.gameObject.tag == "Note")
            {
                Touch_Objects.Add(collision.gameObject);
            }
        }
            if (ChangeTouch == true)
        {
            if (collision.gameObject.tag == "Note")
            {
                Touch_Object = collision.gameObject;
                ChangeTouch = false;
            }
            else if(collision.gameObject.tag == "PlatForm")
            {
                touch_Name = collision.GetComponent<Change_Name_Item>().ItemName();
                TouchJudge = true;
            }
                
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Note")
        {           
            if (Duplication_Object == null)
            {
                TouchJudge = false;
                Touch_Object.GetComponent<ChangeMetronome>().DontTouchMetronome();
                Touch_Object = null;
            }
            else 
            {
                TouchJudge = true;
                Touch_Object.GetComponent<ChangeMetronome>().DontTouchMetronome();
                Touch_Object = Duplication_Object;
                Duplication_Object = null;
            }
            

        }
        else if(collision.gameObject.tag == "PlatForm")
        {
            Touch_Object.GetComponent<Change_Name_Item>().Collision();
            Touch_Object = null;
            TouchJudge = false;
            touch_Name = null;
            ChangeFlavor("None");
            ChangeHeader("None");
            ChangeMovie("None");
            //ChangeTarget = true;           
        }
        else 
        {
            TouchJudge = false;
            Touch_Object = null;
        }


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        FreezeCountTime += 1;
        if (FreezeCountTime < FreezeTime)
        {
            return;            
        }
        else if (FreezeCountTime >= FreezeTime)
        {
            if(Touch_Objects.Count >= 2)
            {
                GameObject touch = Touch_Objects[0];
                Vector3 sabun1 = touch.transform.position - this.transform.position;
                if(sabun1.x < 0)
                {
                    sabun1.x *= -1.0f;
                }
                Debug.Log(sabun1 + "object_0");
                touch = Touch_Objects[1];
                Vector3 sabun2 = touch.transform.position - this.transform.position;
                if (sabun2.x < 0)
                {
                    sabun2.x *= -1.0f;
                }
                Debug.Log(sabun2 + "object_1");
                if(sabun1.x > sabun2.x)
                {
                    Touch_Object = Touch_Objects[0];
                    Duplication_Object = Touch_Objects[1];
                }
                else if(sabun1.x < sabun2.x)
                {
                    Touch_Object = Touch_Objects[1];
                    Duplication_Object = Touch_Objects[0];
                }
                Debug.Log(this.transform.position + "hand");
                Touch_Objects.Clear();
                //Debug.Break();

                Touch_Object.GetComponent<ChangeMetronome>().TouchMetronome();
                Duplication_Object.GetComponent<ChangeMetronome>().DontTouchMetronome();
            }
            else if(Touch_Objects.Count >0)
            {
                Touch_Object = Touch_Objects[0];
                Touch_Objects.Clear();
                Touch_Object.GetComponent<ChangeMetronome>().TouchMetronome();
            }

            if (Touch_Object != null)
            {
                if (Touch_Object.tag == "Note")
                {
                    //Touch_Object.GetComponent<ChangeMetronome>().TouchMetronome();
                }
            }



            if (Check == false)
            {
                FistNoteChange = InputRhythm.instance.Ref_FirstScene();
                if (FistNoteChange == false)
                {
                    NextSceneName.instance.Change_SceneNumber(NextSceneName.SceneNumber.Button_L_Scene);
                    InputRhythm.instance.SetSceneName(NextSceneName.instance.Ref_NextSceneName());
                    InputRhythm.instance.ArrayAction(ActionFolder.instance.Ref_Action(0));
                    InputRhythm.instance.ChangeNoteBox();
                    FistNoteChange = true;
                }
                Check = true;
            }
            FreezeCountTime = FreezeTime;

            //掴んでいるときの画像を変更する
            if (DragAndDrop == true)
            {
                if (this.transform.childCount != 0)
                {
                    this.transform.GetChild(0).gameObject.SetActive(false);
                    this.transform.GetChild(1).gameObject.SetActive(true);
                    this.GetComponent<BoxCollider2D>().offset = ShiftOffset;
                }
            }
            else
            {
                if (this.transform.childCount != 0)
                {
                    this.transform.GetChild(0).gameObject.SetActive(true);
                    this.transform.GetChild(1).gameObject.SetActive(false);
                    this.GetComponent<BoxCollider2D>().offset = Vector2.zero;
                }
            }

            //掴んでいないでMenuボタンを押したとき
            if (DragAndDrop == false && SetInputManager.instance.Ref_Button(SetInputManager.BUTTON.MENU_BUTTON))
            {
                SceneChangeManager.instance.SceneTransition("02_Movie");
            }

            //掴んでいないでL1ボタンを押したとき
            if (DragAndDrop == false && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.L1_BUTTON))
            {
                InputRhythm.instance.ArrayAction(ActionFolder.instance.Ref_Action(0));
                InputRhythm.instance.ChangeNoteBox();
                NextSceneName.instance.Change_SceneNumber(NextSceneName.SceneNumber.Button_L_Scene);
                InputRhythm.instance.SetSceneName(NextSceneName.instance.Ref_NextSceneName());
                //if (SetInputManager.instance.Ref_LongPush_Button(SetInputManager.BUTTON.L1_BUTTON))
                //{

                //}
            }
            //掴んでいないでR1ボタンを押したとき
            else if (DragAndDrop == false && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.R1_BUTTON))
            {
                InputRhythm.instance.ArrayAction(ActionFolder.instance.Ref_Action(1));
                InputRhythm.instance.ChangeNoteBox();
                NextSceneName.instance.Change_SceneNumber(NextSceneName.SceneNumber.Button_R_Scene);
                InputRhythm.instance.SetSceneName(NextSceneName.instance.Ref_NextSceneName());
            }

            //触れていてBボタンを押したとき
            if (DragAndDrop == false && TouchJudge == true && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.B_BUTTON))
            {
                DeleteNote();
            }

            //触れていてAボタンを押した瞬間
            if (TouchJudge == true && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.A_BUTTON))
            {
                CatchIcon();
            }
            //掴んでいる状態で触れたおらず、Aボタンを離したとき
            else if (TouchJudge == false && DragAndDrop == true && !SetInputManager.instance.Ref_LongPush_Button(SetInputManager.BUTTON.A_BUTTON))
            {
                DragIcon();
            }
            //掴んでいる状態で触れていて、Aボタンを離したとき
            else if (DragAndDrop == true && TouchJudge == true && !SetInputManager.instance.Ref_LongPush_Button(SetInputManager.BUTTON.A_BUTTON))
            {
                DropIcon();
            }

            float Horizon = SetInputManager.instance.Ref_Stick_Horizon(SetInputManager.BUTTON.LEFT_STICK);
            float Vertical = SetInputManager.instance.Ref_Stick_Vertical(SetInputManager.BUTTON.LEFT_STICK);

            if (Horizon == 0.0f && Vertical == 0.0f)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
            {
                CollisionCheck = false;
            }
            else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
            {
                CollisionCheck = true;
                MoveChange(Horizon, Vertical);
                RectTransform HandTransform = this.GetComponent<RectTransform>();
                HandTransform.localPosition = HandPosition;
                if (DragAndDrop == true)
                {
                    DragAndDrop_Object.GetComponent<RectTransform>().localPosition = HandPosition + IconShift;
                }
            }
        }
    }

    void MoveChange(float horizon,float vertical)    
    {
        HandPosition.x = HandPosition.x + MoveSpeed * horizon;
        if(HandPosition.x > MaxMovePosition.x)
        {
            HandPosition.x = MaxMovePosition.x;
        }
        else if(HandPosition.x < MinMovePosition.x)
        {
            HandPosition.x = MinMovePosition.x;
        }
        HandPosition.y = HandPosition.y + MoveSpeed * vertical;
        if (HandPosition.y > MaxMovePosition.y)
        {
            HandPosition.y = MaxMovePosition.y;
        }
        else if (HandPosition.y < MinMovePosition.y)
        {
            HandPosition.y = MinMovePosition.y;
        }

    }

    //アイコンを掴んだ時の行動
    void CatchIcon()
    {
        if (touch_Name != null)
        {
            if (DragAndDrop == false)
            {
                CreateCloneIcon();
                DragAndDrop = true;
                TouchJudge = true;
                //ChangeTarget = true;
            }
            else
            {
               
            }
        }       
    }

    //アイコンを掴んでいるときの行動
    void DragIcon()
    {
        DragAndDrop = false;       
        Destroy(DragAndDrop_Object);
    }

    //アイコン掴んでいるときに離したときの行動
    void DropIcon()
    {
        if (Touch_Object.tag == "Note")
        {
            if (DragAndDrop == true)
            {
                Touch_Object.GetComponent<ChangeMetronome>().StorageIcon(DragAndDrop_Object.name);
            }
            else
            {
                Touch_Object.GetComponent<ChangeMetronome>().StorageIcon("None");
            }

            Destroy(DragAndDrop_Object);
            ChangeTouch = true;
            TouchJudge = true;
            DragAndDrop = false;
        }
        
    }

    //メトロノームの中身変更
    void DeleteNote()
    {
        if (Touch_Object.tag == "Note")
        {
            Touch_Object.GetComponent<ChangeMetronome>().StorageIcon("None");
        }
    }

    //掴んでるときのアイコン作成
    void CreateCloneIcon()
    {
        Vector3 IconPosition = HandPosition + IconShift;
        GameObject CloneIconBox = null;
        CloneIconBox = Instantiate(AssetManager.Instance.PrefabObject[0], IconPosition, Quaternion.identity);
        CloneIconBox.transform.SetParent(GameObject.Find("Canvas").transform, false);
        CloneIconBox.transform.SetParent(GameObject.Find("Set_anim").transform, false);
        CloneIconBox.transform.SetSiblingIndex(20);
        CloneIconBox.name = touch_Name;
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
        DragAndDrop_Object = CloneIconBox;
    }

    //説明映像変更
    void ChangeMovie(string Name)
    {
        GameObject MovieObject = GameObject.Find("Movie");
        switch (Name)
        {
            case ("Umbrella"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Umbrella);
                break;

            case ("Recorder"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Recorder);
                break;

            case ("Eraser"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Eraser);
                break;

            case ("Sacrifice"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Sacrifice);
                break;

            case ("AirCannon"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.AirCannon);
                break;

            case ("Bag"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Bag);
                break;

            case ("Ruler"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Ruler);
                break;

            case ("Whistle"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Whistle);
                break;

            case ("None"):
                MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.None);
                break;

            default:
                break;
        }
    }

    //説明文変更
    void ChangeFlavor(string Name)
    {
        GameObject Flavor = GameObject.Find("Flavor");
        switch (Name)
        {
            case ("Umbrella"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.Umbrella);
                break;

            case ("Recorder"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.Recorder);
                break;

            case ("Eraser"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.Eraser);
                break;

            case ("Sacrifice"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.Sacrifice);
                break;

            case ("AirCannon"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.AirCannon);
                break;

            case ("Bag"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.Bag);
                break;

            case ("Ruler"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.Ruler);
                break;

            case ("Whistle"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.Whistle);
                break;

            case ("None"):
                Flavor.GetComponent<ChangeFlavor>().Change(RhythmManager.RhythmAction.None);
                break;

            default:
                break;
        }
    }

    //説明の文字部分変更
    void ChangeHeader(string Name)
    {
        GameObject Header = GameObject.Find("Header");
        switch (Name)
        {
            case ("Umbrella"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.Umbrella);
                break;

            case ("Recorder"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.Recorder);
                break;

            case ("Eraser"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.Eraser);
                break;

            case ("Sacrifice"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.Sacrifice);
                break;

            case ("AirCannon"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.AirCannon);
                break;

            case ("Bag"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.Bag);
                break;

            case ("Ruler"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.Ruler);
                break;

            case ("Whistle"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.Whistle);
                break;

            case ("None"):
                Header.GetComponent<ChangeHeader>().Change(RhythmManager.RhythmAction.None);
                break;

            default:
                break;
        }
    }
}