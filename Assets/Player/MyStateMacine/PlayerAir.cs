﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class PlayerAir : PlayerIF
{

    public PlayerAir(PlayerIF oldPlayer)
    {
        PlayerAnim.instans.Anim.SetTrigger("Jump");
        CopyPlayer(oldPlayer);
    }
    ~PlayerAir()
    {

    }

    // Start is called before the first frame update
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
        SlowDown(AIR_VEL_MULTI,GROUND_VEL_MULTI);
        //自由落下
        Fall();
        //横移動
        MoveX(MAX_AIR_SPEED,ADD_AIR_SPEED);
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
        if (isGround)
        {
            if (Mathf.Abs(SelfVel.x) >= STAND_SPEED)
            {
                NextPlayerState = PLAYER_STATE.RUN;
            }
            else
            {
                NextPlayerState = PLAYER_STATE.STAND;
            }
        }
    }
}
