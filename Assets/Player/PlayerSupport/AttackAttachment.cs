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
            if(other.GetComponent<Enemy>().inScreen)
            {
                other.GetComponent<Enemy>().SetOuterState(ENEMY_STATE.DEAD);
                //ベクトル計算
                Vector3 Obj_toE_Vec = other.transform.position - transform.position;

                other.GetComponent<Enemy>().SetEnemyDeadData(Obj_toE_Vec.normalized);
            }
        }
    }
}
