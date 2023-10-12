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

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        if (other.GetComponent<Enemy>())
        {
            //switch(other.GetComponent<Enemy>.)
            Destroy(other.gameObject);
        }
    }
}
