using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAttachment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().SetOuterState(ENEMY_STATE.DEAD);
        }
    }
}
