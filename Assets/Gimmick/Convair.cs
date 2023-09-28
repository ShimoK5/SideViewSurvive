using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Convair : MonoBehaviour
{
    enum DIRECTION 
    {Right,
    Left}

    [Header("コンベアの向き")]
    [SerializeField] DIRECTION Direction;

    [Header("コンベアの速さ")]
    [SerializeField] float Speed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //向きによる符号
        int TempDirection;
        if(Direction== DIRECTION.Right)
        {
            TempDirection = 1;
        }
        else
        {
            TempDirection = -1;
        }



        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().SetOuterVel(TempDirection * Speed, 0, false, false,true,false);
        }
    }
}
