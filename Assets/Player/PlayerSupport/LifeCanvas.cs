using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCanvas : MonoBehaviour
{
    public GameObject[] LifeArray = new GameObject[3];
    int OldHitPoint = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        int NowHitPoint = Player.instance.HitPoint;

        if(OldHitPoint != NowHitPoint)
        {
            for(int i = 0; i < 3; i++)
            {
                if (NowHitPoint <= i)
                {
                    LifeArray[i].GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }

        OldHitPoint = Player.instance.HitPoint;
    }
}
