using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDelete : MonoBehaviour
{
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
       
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        //if (other.GetComponent<Enemy>() && GameStateManager.instance.GameState == GAME_STATE.Game)
        //{
        //    //switch(other.GetComponent<Enemy>.)
        //    if(other.GetComponent<Enemy>())
        //        Destroy(other.gameObject);
        //}
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Enemy>() && GameStateManager.instance.GameState == GAME_STATE.Game)
    //    {
    //        if (other.GetComponent<Enemy>().stationary)
    //        {
    //            other.GetComponent<Enemy>().Operation = true;
    //        }
    //    }
    //}
}
