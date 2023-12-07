using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPress : EnemyIF
{
    int MoveFlameCount = 0;
    int PressDelay = -1;
    bool Press = false; 

    public EnemyPress(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
    }

    public override void CustumUpdate()
    {

    }

    public override void CustumFixed()
    {
        //過去情報保存
        KeepOld();
        //勢い減少 
        SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
        //自由落下
        //Fall();
        //縦移動
        MoveFlameCount++;

        if (MoveFlameCount > 300)
        {
            MoveFlameCount = 0;
            PressDelay = 40;
        }
        else if(PressDelay >= 0)
        {
            if(PressDelay == 0)
                Press = true;

            PressDelay--;
            SelfVel.x = 0;
        }
        
        if(MoveFlameCount != 0)
        {
            Vector3 playerPos = Player.instance.transform.position;
            float sign = Mathf.Sign(playerPos.x - tf.position.x);
            SelfVel.x = 8 * (1.0f / 120) * 0.9f * sign;
        }




        //ジャンプ処理
        //Jump();
        //状態遷移
        ChangeNextState();
        //速度反映
        MoveEnemy();
        //フラグリセット
        //JumpKeyDown = false;
        //共通更新
        FixedCommon();
    }
}
