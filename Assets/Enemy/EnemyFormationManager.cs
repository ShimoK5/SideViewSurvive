using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationManager : MonoBehaviour
{
    public Enemy[] EnemyFormations;
    int  Cnt = 0;
    bool AllInScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        Cnt = 0;
        AllInScreen = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool TempAllInScreen = true;

        for(int i = 0; i < EnemyFormations.Length; i++ )
        {
            //エネミーあれば
            if(EnemyFormations[i])
            {
                //映っていれば
                if(EnemyFormations[i].inScreen)
                {

                }
                //映っていなければ
                else
                {
                    TempAllInScreen = false;
                }
            }
        }

        //一度でも全員映ってたらずっとtrue
        if(TempAllInScreen)
        {
            AllInScreen = true;
        }

        if(AllInScreen)
        {

            Cnt++;

            if(Cnt == 90)
            {
                for (int i = 0; i < EnemyFormations.Length; i++)
                {
                    //エネミーあれば
                    if (EnemyFormations[i])
                    {
                        //Goに変える
                        EnemyFormations[i].GetM_Enemy().Go();
                    }
                }

                //信号送ったら破棄
                Destroy(gameObject);
            }

            
        }
    }
}
