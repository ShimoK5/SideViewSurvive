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
        //大元はfalse
        bool TempAllInScreen = false;

        if(GameStateManager.instance.GameState == GAME_STATE.Game)
        {

            //デフォはtrue
            TempAllInScreen = true;

            for (int i = 0; i < EnemyFormations.Length; i++)
            {
                //エネミーあれば
                if (EnemyFormations[i])
                {
                    //映っていれば
                    if (EnemyFormations[i].inScreen)
                    {

                    }
                    //映っていなければ
                    else
                    {
                        TempAllInScreen = false;
                    }
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

            for (int i = 0; i < EnemyFormations.Length; i++)

            {
                if (Cnt == 90 + i * 10)
                {

                    //エネミーあれば
                    if (EnemyFormations[i])
                    {
                        //Goに変える
                        EnemyFormations[i].GetM_Enemy().Go();
                    }

                    if(i == EnemyFormations.Length - 1)
                    {
                        //全部信号送ったら破棄
                        Destroy(gameObject);
                    }
                }
            }
                

            
        }
    }
}
