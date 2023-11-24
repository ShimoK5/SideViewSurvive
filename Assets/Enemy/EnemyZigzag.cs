﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class EnemyZigzag : EnemyIF
{
    //private int FlameCount = 0;
    bool isUP = false;

    public EnemyZigzag(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
        SelfVel = new Vector2(-MAX_RUN_SPEED, MAX_RUN_SPEED);
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
        //移動
        //FlameCount++;
        //if (FlameCount > 120)
        //    FlameCount = 0;

        if (RhythmManager.Instance.FCnt % 60 == 0)
            isUP = !isUP;

        if(isUP)
            SelfVel = new Vector2(-MAX_RUN_SPEED, MAX_RUN_SPEED);
        else
            SelfVel = new Vector2(-MAX_RUN_SPEED, -MAX_RUN_SPEED);


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
        //Debug.Log("床");
        //isGround = true;
        //StandBlock = block;
        float YPos = block.transform.position.y + (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        SelfVel.y = 0.0f;
        OtherVel.y = 0.0f;
    }
}
