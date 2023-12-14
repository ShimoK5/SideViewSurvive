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
    [SerializeField] private Vector3 HandPosition = Vector3.zero;                           //ハンドの初期位置
    private Vector3 IconShift = new Vector3(-40.0f,0.0f,0.0f);                                               //アイコンの位置ずれ
    private Vector2 ShiftOffset = new Vector2(-30.0f, 0.0f);                                //当たり判定のずれ
    private Vector2 MoveR1Position = new Vector2(-750.0f,-850.0f);                          //R1押したときの移動場所
    private Vector2 MoveL1Position = new Vector2(-1050.0f, -850.0f);                        //L1押したときの移動場所
    private Vector2 MaxMovePosition = new Vector2(650.0f, 100.0f);
    private Vector2 MinMovePosition = new Vector2(-600.0f, -900.0f);
    [SerializeField] private float MoveSpeed = 10.0f;                                        //移動スピード
    [SerializeField] private int FreezeTime = 180;                                          //最初に動かせなくする用
    [SerializeField] private int CountTime = 0;                                             //セット画面が始まってからのカウント
    [SerializeField] private string touch_Name = null;                                      //触れたオブジェクトを判断するよう
    [SerializeField] private GameObject Touch_Object = null;                                //触ったオブジェクト
    [SerializeField] private GameObject Duplication_Object = null;                          //オブジェクトが重なってしまった場合
    [SerializeField] private GameObject DragAndDrop_Object = null;                           //掴んでいるオブジェクト
    [SerializeField] private bool TouchJudge = false;                                       //触っているかどうか
    [SerializeField] private bool DragAndDrop = false;                                      //アイコン掴んでいるかどうか
    [SerializeField] bool FistNoteChange = false;                                            //ノートにぶつかっているか確認用
    //[SerializeField] bool ChangeTarget = false;                                              //シーンチェンジ用

    
    bool ChangeTouch = false;

   

    // Start is called before the first frame update

    void Start()
    {
        HandPosition = new Vector3(0.0f, 0.0f, -10.0f);
        FreezeTime = 180;
        CountTime = 0;
        touch_Name = null;
        RectTransform HandTransform = this.GetComponent<RectTransform>();
        HandTransform.TransformPoint(HandPosition);
        DragAndDrop = false;
        FistNoteChange = true;
        GameObject MovieObject = GameObject.Find("Movie");
        MovieObject.GetComponent<MovieChange>().Change(RhythmManager.RhythmAction.Whistle);
        ChangeFlavor("None");
        ChangeHeader("None");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Note" )
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
        if(ChangeTouch == true)
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
                Touch_Object = null;
            }
            else 
            {
                TouchJudge = true;
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
        CountTime += 1;
        if (CountTime < FreezeTime)
        {
            return;
        }
        else if (CountTime >= FreezeTime)
        {
            if (FistNoteChange == true)
            {
                InputRhythm.instance.ArrayAction(ActionFolder.instance.Ref_Action(0));
                InputRhythm.instance.ChangeNoteBox();               
                FistNoteChange = false;
            }
            CountTime = FreezeTime;

            //CustomTimeLine.instance.StopTimeLine();

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

                //if(ChangeTarget == true)
                //{
                //    Debug.Log("1");

                //}
            }

            if (DragAndDrop == false && SetInputManager.instance.Ref_Button(SetInputManager.BUTTON.MENU_BUTTON))         //掴んでいないでMenuボタンを押したとき
            {
                SceneChangeManager.instance.SceneTransition(NextSceneName.Instance.Ref_NextSceneName());
            }

            if (DragAndDrop == false && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.L1_BUTTON))       //掴んでいないでL1ボタンを押したとき
            {
                HandPosition.x = MoveL1Position.x;
                HandPosition.y = MoveL1Position.y;
                RectTransform HandTransform = this.GetComponent<RectTransform>();
                HandTransform.localPosition = HandPosition;
            }
            else if (DragAndDrop == false && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.R1_BUTTON))  //掴んでいないでR1ボタンを押したとき
            {
                HandPosition.x = MoveR1Position.x;
                HandPosition.y = MoveR1Position.y;
                RectTransform HandTransform = this.GetComponent<RectTransform>();
                HandTransform.localPosition = HandPosition;
            }

            if (TouchJudge == true && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.B_BUTTON))          //触れていてBボタンを押したとき
            {
                DeleteNote();
            }

            if (TouchJudge == true && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.A_BUTTON))
            {//触れていてAボタンを押した瞬間
                CatchIcon();
            }
            else if (TouchJudge == false && DragAndDrop == true && !SetInputManager.instance.Ref_LongPush_Button(SetInputManager.BUTTON.A_BUTTON))
            {//掴んでいる状態で触れたおらず、Aボタンを離したとき
                DragIcon();
            }
            else if (DragAndDrop == true && TouchJudge == true && !SetInputManager.instance.Ref_LongPush_Button(SetInputManager.BUTTON.A_BUTTON))
            {//掴んでいる状態で触れていて、Aボタンを離したとき
                DropIcon();
            }

            float Horizon = SetInputManager.instance.Ref_Stick_Horizon(SetInputManager.BUTTON.LEFT_STICK);
            float Vertical = SetInputManager.instance.Ref_Stick_Vertical(SetInputManager.BUTTON.LEFT_STICK);

            if (Horizon == 0.0f && Vertical == 0.0f)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
            {
            }
            else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
            {
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

    void CatchIcon()
    {
        if (touch_Name != null)
        {
            if (DragAndDrop == false)
            {
                CreateCloneIcon();
                DragAndDrop = true;
                TouchJudge = false;
                //ChangeTarget = true;
            }
            else
            {
               
            }
        }       
    }

    void DragIcon()
    {
        DragAndDrop = false;       
        Destroy(DragAndDrop_Object);
        //Debug.Log("2");
    }

    void DropIcon()
    {
        //Debug.Log("DropIcon");
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

    void DeleteNote()
    {
        if (Touch_Object.tag == "Note")
        {
            Touch_Object.GetComponent<ChangeMetronome>().StorageIcon("None");
        }
    }

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