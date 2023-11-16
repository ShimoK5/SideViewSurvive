using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Spine;

public class PlayerRun : PlayerIF
{
    TrackEntry m_TrackEntry = null;

    bool DoneRightStep = false;
    bool DoneLeftStep = false;

    public PlayerRun(PlayerIF oldPlayer)
    {
        if (Player.instance.GetAnim().Anim.AnimationName != "normal/run")
        {
            m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/run", true);
            m_TrackEntry.Complete += FlagReset;
        }

        CopyPlayer(oldPlayer);
        DoneRightStep = false;
        DoneLeftStep = false;

    }

    public override void CustumUpdate()
    {
        //if (Input.GetKeyDown(jumpKey)) // キー入力判定
        //{
        //    JumpKeyDown = true;
        //}
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
        //JumpKeyDown = false;
        //共通更新
        FixedCommon();

        //足音SE
        if (m_TrackEntry != null)
        {
            if(!DoneRightStep)
            {
                if (m_TrackEntry.TrackTime > 0.0f)
                {
                    DoneRightStep = true;
                    //SoundManager.instance.SEStop();
                    SoundManager.instance.SEPlay("仮SE");
                }
            }
            if (!DoneLeftStep)
            {
                if (m_TrackEntry.TrackTime > 0.0f + 0.333f)
                {
                    DoneLeftStep = true;
                    //SoundManager.instance.SEStop();
                    SoundManager.instance.SEPlay("仮SE");
                }
            }
        }

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

    //フラグリセット
    void FlagReset(TrackEntry trackEntry)
    {
        
        DoneRightStep = false;
        DoneLeftStep = false;
    }

}
