﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MoveHand : MonoBehaviour
{
    [SerializeField] private Vector3 HandPosition = Vector3.zero;                           //ハンドの初期位置
    [SerializeField] private float MoveSpeed = 5.0f;                                        //移動スピード
    [SerializeField] private GameObject Touch_Object = null;                                //触ったオブジェクト
    [SerializeField] private GameObject Duplication_Object = null;                          //オブジェクトが重なってしまった場合
    [SerializeField] private GameObject DragAndDrop_Object = null;                           //掴んでいるオブジェクト
    [SerializeField] private bool TouchJudge = false;                                       //触っているかどうか
    [SerializeField] private bool DragAndDrop = false;                                      //アイコン掴んでいるかどうか
    [SerializeField] bool NoteCollision = false;                                            //ノートにぶつかっているか確認用
    [SerializeField] bool ChangeScene = false;                                              //シーンチェンジ用
    [SerializeField] bool MovieNoise = false;                                               //ノイズ発生させたいかどうか（消す予定）
    [SerializeField] int ChangeMovieFlame = 0;                                              //シーンの変化に対応したフレーム数　消す予定）
    [SerializeField] GameObject MovieObject;
    [SerializeField] private MovieChange Movie = null;                                      //ムービー変化する用   
    [SerializeField] public GameObject[] NoteBox = new GameObject[8];                       //ノートボックスを格納する用
    [SerializeField] Material Ma;

    bool Changetouch = false;

    private Color Translucent;                                                              //表示する色が薄くなる用の変数保管用
    private Color entity;                                                                   //表示する色が濃くなる用の変数保管用

    // Start is called before the first frame update

    void Start()
    {
        HandPosition = new Vector3(0.0f, 0.0f, -10.0f);
        RectTransform HandTransform = this.GetComponent<RectTransform>();
        HandTransform.TransformPoint(HandPosition);
        DragAndDrop = false;
        NoteCollision = false;
        Movie = MovieObject.GetComponent<MovieChange>();
        Movie.Change(RhythmManager.RhythmAction.None);
        ChangeFlavor("None");
        ChangeHeader("None");
        Translucent = Color.white;
        Translucent.a = 0.5f;
        entity = Color.white;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Note" )
        {
            if (TouchJudge == false)
            {
                TouchJudge = true;
                Touch_Object = collision.gameObject;
                Touch_Object.GetComponent<Image>().color = entity;
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
            ChangeFlavor(Touch_Object.name);
            ChangeHeader(Touch_Object.name);
        }
        else
        {
            ChangeScene = false;            
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(Changetouch == true)
        {
            Touch_Object = collision.gameObject;
            Changetouch = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Note")
        {           
            if (Duplication_Object == null)
            {
                Touch_Object.GetComponent<Image>().color = Translucent;
                TouchJudge = false;
                Touch_Object = null;
            }
            else 
            {
                TouchJudge = true;
                Touch_Object.GetComponent<Image>().color = Translucent;
                Touch_Object = Duplication_Object;
                Touch_Object.GetComponent<Image>().color = entity;
                Duplication_Object = null;
            }
            
        }
        else if (collision.gameObject.name == "Target")
        {
            TouchJudge = false;
            collision.GetComponent<TargetCollision>().DontTouchImage();
            Touch_Object = null;
            Movie.Change(RhythmManager.RhythmAction.None);
            ChangeFlavor("None");
            ChangeHeader("None");
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
        if(SetInputManager.instance.Ref_Button(SetInputManager.BUTTON.MENU_BUTTON))
        {
            SceneChangeManager.instance.SceneTransition("ShimokawaraScene 1");
        }

        if(DragAndDrop == false && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.L1_BUTTON))
        {
            HandPosition.x = -270.0f;
            HandPosition.y = -171.0f;
            RectTransform HandTransform = this.GetComponent<RectTransform>();
            HandTransform.localPosition = HandPosition;
        }
        else if(DragAndDrop == false && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.R1_BUTTON))
        {
            HandPosition.x = -130.0f;
            HandPosition.y = -171.0f;
            RectTransform HandTransform = this.GetComponent<RectTransform>();
            HandTransform.localPosition = HandPosition;
        }

        if(TouchJudge == true && SetInputManager.instance.Ref_Trigger_Button(SetInputManager.BUTTON.B_BUTTON))
        {
            DeleteNote();
        }


        if(TouchJudge == true && SetInputManager.instance.Ref_Button(SetInputManager.BUTTON.A_BUTTON))
        {
            CatchIcon();
        }
        else if(TouchJudge == false && DragAndDrop == true && !SetInputManager.instance.Ref_LongPush_Button(SetInputManager.BUTTON.A_BUTTON))
        {
            DragIcon();
        }
        else if (DragAndDrop == true && TouchJudge == true && !SetInputManager.instance.Ref_LongPush_Button(SetInputManager.BUTTON.A_BUTTON))
        {
            DropIcon();
        }


        float Horizon = SetInputManager.instance.Ref_Stick_Horizon(SetInputManager.BUTTON.LEFT_STICK);
        float Vertical = SetInputManager.instance.Ref_Stick_Vertical(SetInputManager.BUTTON.LEFT_STICK);

        if (Horizon == 0.0f && Vertical == 0.0f)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
        }
        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            MoveChange(Horizon,Vertical);
            RectTransform HandTransform = this.GetComponent<RectTransform>();
            HandTransform.localPosition = HandPosition;
            if (DragAndDrop == true)
            {
                DragAndDrop_Object.GetComponent<RectTransform>().localPosition = HandPosition;
            }           
        }

        if(MovieNoise == true)
        {
            Ma = MovieObject.GetComponent<RawImage>().material;
            Ma.SetFloat("_BoolSwitch",1.0f);
            Ma.EnableKeyword("FILL_WITH_RED");
            ChangeMovieFlame += 1;
            if (ChangeMovieFlame > 60)
            {
                Ma.SetFloat("_BoolSwitch", 0.0f);
                ChangeMovieFlame = 0;
                MovieNoise = false;
                Ma.DisableKeyword("FILL_WITH_RED");
            }
        }
       

        

    }

    void MoveChange(float horizon,float vertical)    
    {
        HandPosition.x = HandPosition.x + MoveSpeed * horizon;
        HandPosition.y = HandPosition.y + MoveSpeed * vertical;
    }

    void CatchIcon()
    {
        if (Touch_Object.tag == "Icon")
        {
            if (DragAndDrop == false)
            {
                CreateCloneIcon();
                DragAndDrop = true;
                TouchJudge = false;
            }
            else
            {
               
            }
        }       
    }

    void DragIcon()
    {
        Debug.Log("DragIcon");
        DragAndDrop = false;
        Destroy(DragAndDrop_Object);
    }

    void DropIcon()
    {
        Debug.Log("DropIcon");
        if (Touch_Object.tag == "Note")
        {
            if (DragAndDrop == true)
            {
                switch (DragAndDrop_Object.name)
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

            Destroy(DragAndDrop_Object);
            Changetouch = true;
            TouchJudge = true;
            DragAndDrop = false;
        }
       
    }

    void DeleteNote()
    {
        if (Touch_Object.tag == "Note")
        {
            InputRhythm.instance.ChangeNoteBox(RhythmManager.RhythmAction.None, Touch_Object.name);
        }
    }

    void CreateCloneIcon()
    {    
        GameObject CloneIconBox = null;
        CloneIconBox = Instantiate(AssetManager.Instance.PrefabObject[0], HandPosition, Quaternion.identity);
        CloneIconBox.transform.SetParent(GameObject.Find("SetRhythmUI").transform, false);
        CloneIconBox.transform.SetSiblingIndex(14);
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
        DragAndDrop_Object = CloneIconBox;
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

            case ("None"):
                Movie.Change(RhythmManager.RhythmAction.None);
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