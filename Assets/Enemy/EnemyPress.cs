using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPress : EnemyIF
{
    int FlameCount = 0;
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
        FlameCount++;
        if (FlameCount > 300)
            FlameCount = 0;

        if (FlameCount < 60)
            SelfVel.y = -MAX_RUN_SPEED;
        else if (FlameCount > 60)
            SelfVel.y = MAX_RUN_SPEED;

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
