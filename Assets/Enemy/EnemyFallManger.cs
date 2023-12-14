using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallManger : MonoBehaviour
{
    [SerializeField] public Enemy[] EnemyFalls;
    bool AllInScreen = false;
    bool Fall = false;
    float EnemyLeftPos;

    // Start is called before the first frame update
    void Start()
    {
        AllInScreen = false;
        EnemyLeftPos = EnemyFalls[0].transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        bool TempAllInScreen = true;

        for (int i = 0; i < EnemyFalls.Length; i++)
        {
            //エネミーあれば
            if (EnemyFalls[i])
            {
                //映っていなければ
                if (!EnemyFalls[i].inScreen)
                {
                    TempAllInScreen = false;
                }
            }
        }

        //一度でも全員映ってたらずっとtrue
        if (TempAllInScreen)
        {
            AllInScreen = true;
        }

        if (EnemyLeftPos <
            Player.instance.transform.position.x)
        {
            Fall = true;
        }

        if (AllInScreen && Fall)
        {
            for (int i = 0; i < EnemyFalls.Length; i++)
            {
                //エネミーあれば
                if (EnemyFalls[i])
                {
                    //Fallに変える
                    EnemyFalls[i].GetM_Enemy().Drop();
                }

                if (i == EnemyFalls.Length - 1)
                {
                    //全部信号送ったら破棄
                    Destroy(gameObject);
                }

            }
        }
    }
}
