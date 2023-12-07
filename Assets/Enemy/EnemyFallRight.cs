using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallRight : EnemyIF
{
    public EnemyFallRight(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
