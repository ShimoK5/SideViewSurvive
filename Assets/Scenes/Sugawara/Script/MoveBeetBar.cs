using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeetBar : MonoBehaviour
{
    [SerializeField] public RhythmManager Manager = null;
    [SerializeField] public int Tempo = 0;
    float Init_BarPosition_x = -113.5f;
    Vector3 BarPosition = new Vector3(-113.5f, -195.0f, 0.0f);
    [SerializeField] RectTransform BarTransForm = null;
    float Move_BarSpeed = 0.0f;
    [SerializeField] public GameObject rhythm = null;

    // Start is called before the first frame update
    void Start()
    {
        BarTransForm = GetComponent<RectTransform>();
        rhythm = GameObject.Find("RhythmManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rhythm = GameObject.Find("RhythmManager");
        if(Manager == null)
        {
           rhythm = GameObject.Find("RhythmManager");
            Debug.Log("< color =#ff0000ff>rhythm</color>");
            Manager = rhythm.GetComponent<RhythmManager>();
            
           
        }

        //GameObject rhythm = GameObject.Find("RhythmManager");
        //Debug.Log(rhythm);
        //Manager = rhythm.GetComponent<RhythmManager>();
        //Debug.Log(Manager);

        

        Tempo = Manager.FCnt;
        Move_BarSpeed = 100 * (BarTransForm.localScale.x) / Manager.BeatTempo;

        BarPosition.x = Init_BarPosition_x + Move_BarSpeed * Manager.FCnt;
        BarTransForm.localPosition = BarPosition;

    }
}
