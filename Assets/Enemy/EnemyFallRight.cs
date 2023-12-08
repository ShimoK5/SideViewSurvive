using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallRight : EnemyIF
{

    Vector3 StartPos; //初期座標
    FallState state = FallState.Idle;

    public EnemyFallRight(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
    }

    public override void CustumFixed()
    {
        //過去情報保存
        KeepOld();
        //勢い減少 
        //SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
        //自由落下
        switch (state)
        {
            case FallState.Idle:
                break;
            case FallState.Fall:
                {
                    Fall();
                    break;
                }
            case FallState.Walk:
                {
                    Fall();
                    //横移動

                    break;
                }
        }

        if (isGround)
        {
            state = FallState.Walk;
            SelfVel.x = -MAX_RUN_SPEED;
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

    public override void Drop()
    {
        state = FallState.Fall;
    }

    public override void HitUnder(Block block)
    {
        //Debug.Log("床");
        isGround = true;
        StandBlock = block;
        float YPos = block.transform.position.y + (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        SelfVel.y = 0.0f;
        OtherVel.y = 0.0f;
    }

    public override void HitLeft(Block block)
    {
        float XPos = block.transform.position.x + (block.Size.x + Size.x) / 2;
        tf.transform.position = new Vector3(XPos, tf.transform.position.y, tf.transform.position.z);
        SelfVel.x *= -1.0f;
        OtherVel.x = 0.0f;
    }

    public override void HitRight(Block block)
    {
        float XPos = block.transform.position.x - (block.Size.x + Size.x) / 2;
        tf.transform.position = new Vector3(XPos, tf.transform.position.y, tf.transform.position.z);
        SelfVel.x *= -1.0f;
        OtherVel.x = 0.0f;
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

}
