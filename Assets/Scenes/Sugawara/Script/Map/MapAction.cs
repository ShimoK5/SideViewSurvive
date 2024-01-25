using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MapAction : MonoBehaviour
{
    public enum STATE
    {
        Idle = 0,
        Right,
        Left,
    }

    [SerializeField] float Max_Position;
    [SerializeField] float Min_Position;

    public STATE MapState;
    [SerializeField] Image Map;
    [SerializeField] float MoveSpeed;
    [SerializeField] float SpeedUpper = 1.0f;
    [SerializeField] int MoveCount;
    [SerializeField] Vector3 MapPosition = new Vector3(0.0f, 0.0f, 0.0f);
    // Start is called before the first frame update
    void Awake()
    {
        if(Map == null)
        {
            Map = GameObject.Find("One_Map").GetComponent<Image>();            
        }
        if(MoveSpeed <= 1.0)
        {
            MoveSpeed = 1.0f;
        }
        SpeedUpper = 1.0f;
        MapState = STATE.Idle;
        MapPosition = Map.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       MapMove();           //マップ移動用関数
    }


    void MapMove()
    {
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
            MapPosition.x += Horizon * MoveSpeed * SpeedUpper * -1.0f;
            if(Min_Position >= MapPosition.x)
            {
                MapPosition.x = Map.transform.localPosition.x;
            }
            else if (Max_Position <= MapPosition.x)
            {
                MapPosition.x = Map.transform.localPosition.x;
            }
            Map.transform.localPosition = MapPosition;

        }
    }

    public STATE GetState()
    {
        return MapState;
    }
}
