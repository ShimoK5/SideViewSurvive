using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MapAction : MonoBehaviour
{
    [System.Serializable]
    public class MapClass
    {
        public float Max_Position;
        public float Min_Position;
        public GameObject Map;
        //public Image MapImage;
        public Vector3 MapPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public enum STATE
    {
        Idle = 0,
        Right,
        Left,
    }

    public STATE MapState;
    [SerializeField] MapClass EasyMap;
    [SerializeField] MapClass HardMap;
    [SerializeField] float MoveSpeed;
    [SerializeField] float SpeedUpper = 1.0f;
    [SerializeField] int MoveCount;
    bool NowMap = false;
    string stage;

    // Start is called before the first frame update
    void Awake()
    {
        if(EasyMap == null)
        {
            //Map = GameObject.Find("One_Map").GetComponent<Image>();
            EasyMap.Map = GameObject.Find("EasyMap");
            EasyMap.MapPosition = EasyMap.Map.transform.localPosition;
        }
        if(MoveSpeed <= 5.0)
        {
            MoveSpeed = 5.0f;
        }
        SpeedUpper = 1.0f;
        MapState = STATE.Idle;
        NowMap = false;
        stage = "None";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stage != InputRhythm.instance.GetSceneName())
        {
            stage = InputRhythm.instance.GetSceneName();
            NowMap = true;
        }
        MapMove(stage);           //マップ移動用関数
    }


    void MapMove(string StageLevel)
    {
        if(NowMap == true)
        {
            switch (StageLevel)
            {
                case "Game":
                    EasyMap.Map.SetActive(true);
                    EasyMap.MapPosition = EasyMap.Map.transform.localPosition;
                    HardMap.Map.SetActive(false);
                    break;

                case "Game Hard":
                    EasyMap.Map.SetActive(false);
                    HardMap.Map.SetActive(true);
                    HardMap.MapPosition = HardMap.Map.transform.localPosition;
                    break;

                default:
                    EasyMap.Map.SetActive(true);
                    HardMap.Map.SetActive(false);
                    break;
            }
        }
        float Horizon = SetInputManager.instance.Ref_Stick_Horizon(SetInputManager.BUTTON.LEFT_STICK);
        //float Vertical = SetInputManager.instance.Ref_Stick_Vertical(SetInputManager.BUTTON.LEFT_STICK);       
       
        if (Horizon == 0.0f)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
            MoveCount = 0;
            SpeedUpper = 1.0f;
            if (MapState !=  STATE.Idle)
            { 
                MapState = STATE.Idle;
            }

        }
        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            if(Horizon > 0 && MapState != STATE.Right)
            {
                MapState = STATE.Right;
                SpeedUpper = 1.0f;
            }
            else if (Horizon < 0 && MapState != STATE.Left)
            {
                MapState = STATE.Left;
                SpeedUpper = 1.0f;
            }
            MoveCount += 1;
            if(MoveCount >=  60)
            {
                MoveCount = 0;
                SpeedUpper *= 2.0f;
            }
            switch(StageLevel)
            {
                case "Game":
                    EasyMap.MapPosition.x += Horizon * MoveSpeed * SpeedUpper * -1.0f;
                    if (EasyMap.Min_Position >= EasyMap.MapPosition.x)
                    {
                        EasyMap.MapPosition.x = EasyMap.Map.transform.localPosition.x;
                    }
                    else if (EasyMap.Max_Position <= EasyMap.MapPosition.x)
                    {
                        EasyMap.MapPosition.x = EasyMap.Map.transform.localPosition.x;
                    }
                    EasyMap.Map.transform.localPosition = EasyMap.MapPosition;
                    break;

                case "Game Hard":
                    HardMap.MapPosition.x += Horizon * MoveSpeed * SpeedUpper * -1.0f;
                    if (HardMap.Min_Position >= HardMap.MapPosition.x)
                    {
                        HardMap.MapPosition.x = HardMap.Map.transform.localPosition.x;
                    }
                    else if (HardMap.Max_Position <= HardMap.MapPosition.x)
                    {
                        HardMap.MapPosition.x = HardMap.Map.transform.localPosition.x;
                    }
                    HardMap.Map.transform.localPosition = HardMap.MapPosition;
                    break;

                default:
                    EasyMap.MapPosition.x += Horizon * MoveSpeed * SpeedUpper * -1.0f;
                    if (EasyMap.Min_Position >= EasyMap.MapPosition.x)
                    {
                        EasyMap.MapPosition.x = EasyMap.Map.transform.localPosition.x;
                    }
                    else if (EasyMap.Max_Position <= EasyMap.MapPosition.x)
                    {
                        EasyMap.MapPosition.x = EasyMap.Map.transform.localPosition.x;
                    }
                    EasyMap.Map.transform.localPosition = EasyMap.MapPosition;
                    break;
            }
        }
    }

    public STATE GetState()
    {
        return MapState;
    }
}
