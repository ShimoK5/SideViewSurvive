using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : PlayerIF
{
    public PlayerRecorder(PlayerIF oldPlayer)
    {
        PlayerAnim.instans.Anim.SetTrigger("Idle");
        CopyPlayer(oldPlayer);
        SelfVel.x *= ACTION_VEL_MULTI;
        GameObject SoundWaves = (GameObject)Resources.Load("SoundWaves");
        SoundWaves = Instantiate(SoundWaves, rb.transform.position, Quaternion.Euler(90,0,0));
    }

    public override void CustumUpdate()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(jumpKey)) // キー入力判定
            {
                JumpKeyDown = true;
            }
        }
    }

    public override void CustumFixed()
    {
        if (isGround)
        {
            //過去情報保存
            KeepOld();
            //勢い減少
            SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
            //自由落下
            Fall();
            //横移動
            MoveX(MAX_RUN_SPEED * ACTION_VEL_MULTI, ADD_RUN_SPEED * ACTION_VEL_MULTI);
            //ジャンプ処理
            Jump();
            //状態遷移
            //ChangeNextState();
            //速度反映
            MovePlayer();
            //フラグリセット
            JumpKeyDown = false;
            //共通更新
            FixedCommon();
        }
        else
        {
            //過去情報保存
            KeepOld();
            //勢い減少
            SlowDown(AIR_VEL_MULTI, GROUND_VEL_MULTI);
            //自由落下
            Fall();
            //横移動
            MoveX(MAX_AIR_SPEED * ACTION_VEL_MULTI, ADD_AIR_SPEED * ACTION_VEL_MULTI);
            //状態遷移
            //ChangeNextState();
            //速度反映
            MovePlayer();
            //フラグリセット
            JumpKeyDown = false;
            //共通更新
            FixedCommon();
        }
    }

    //状態遷移
    //protected override void ChangeNextState()
    //{
    //    if (Mathf.Abs(SelfVel.x /*+ OtherVel.x*/) >= STAND_SPEED && NextPlayerState == PLAYER_STATE.STAND)
    //    {
    //        NextPlayerState = PLAYER_STATE.RUN;
    //    }
    //    if (!isGround)
    //    {
    //        NextPlayerState = PLAYER_STATE.AIR;
    //    }
    //}
}
