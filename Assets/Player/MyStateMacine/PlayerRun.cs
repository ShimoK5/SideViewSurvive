using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class PlayerRun : PlayerIF
{
    public PlayerRun(PlayerIF oldPlayer)
    {
        if (Player.instance.GetAnim().Anim.AnimationName != "run")
            Player.instance.GetAnim().Anim.state.SetAnimation(0, "run", true);
        CopyPlayer(oldPlayer);
    }

    public override void CustumUpdate()
    {
        if (Input.GetKeyDown(jumpKey)) // キー入力判定
        {
            JumpKeyDown = true;
        }
    }

    public override void CustumFixed()
    {
        //過去情報保存
        KeepOld();
        //勢い減少 
        SlowDown(GROUND_VEL_MULTI,GROUND_VEL_MULTI);
        //自由落下
        Fall();
        //横移動
        MoveX(MAX_RUN_SPEED, ADD_RUN_SPEED);
        //向き変更
        ChangeDirection();
        //ジャンプ処理
        Jump();
        //状態遷移
        ChangeNextState();
        //速度反映
        MovePlayer();
        //フラグリセット
        JumpKeyDown = false;
        //共通更新
        FixedCommon();
    }

    //状態遷移
    protected override void ChangeNextState()
    {
        if (Mathf.Abs(SelfVel.x/* + OtherVel.x*/) < STAND_SPEED && NextPlayerState == PLAYER_STATE.RUN)
        {
            NextPlayerState = PLAYER_STATE.STAND;
        }
        if (!isGround)
        {
            NextPlayerState = PLAYER_STATE.AIR;
        }
    }

}
