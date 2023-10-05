using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class EnemyTracking : EnemyIF
{
    private Vector3 PlayerPos;
    public EnemyTracking(EnemyIF oldEnemy)
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
        Fall();
        //移動
        PlayerPos = Player.instance.transform.position;
        float radius = Mathf.Atan2(PlayerPos.y - tf.transform.position.y, PlayerPos.x - tf.transform.position.x);
        SelfVel.x = Mathf.Cos(radius) * MAX_RUN_SPEED;
        SelfVel.y = Mathf.Sin(radius) * MAX_RUN_SPEED;

        //MoveX(MAX_RUN_SPEED, ADD_RUN_SPEED);
        //MoveY(MAX_RUN_SPEED, ADD_RUN_SPEED);
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

    //状態遷移
    protected override void ChangeNextState()
    {
        //if (Mathf.Abs(SelfVel.x/* + OtherVel.x*/) < STAND_SPEED && NextEnemyState == ENEMY_STATE.RUN)
        //{
        //    NextEnemyState = ENEMY_STATE.STAND;
        //}
        //if (!isGround)
        //{
        //    NextEnemyState = ENEMY_STATE.AIR;
        //}
    }
    public override void HitUnder(Block block)
    {
        Debug.Log("床");
        isGround = true;
        StandBlock = block;
        float YPos = block.transform.position.y + (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        SelfVel.y = 0.0f;
        OtherVel.y = 0.0f;
    }
}

