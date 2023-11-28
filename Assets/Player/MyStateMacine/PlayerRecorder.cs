using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class PlayerRecorder : PlayerIF
{
    TrackEntry m_TrackEntry;

    public PlayerRecorder(PlayerIF oldPlayer)
    {
        //if (Player.instance.GetAnim().Anim.AnimationName != "normal/idle")
            //Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/idle", true);
        CopyPlayer(oldPlayer);

        if (!isGround)
        {
            //
            //空中
            m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "recoder/recoder_jump_attack", true);
        }
        else
        {
            if (Mathf.Abs(SelfVel.x /*+ OtherVel.x*/) >= STAND_SPEED)
            {
                //走り
                m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "recoder/recoder_run_attack", true);
            }
            else
            {
                //立ち
                m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "recoder/recoder_idel_attack", true);

            }
        }

        //減速
        SelfVel.x *= ACTION_VEL_MULTI;
        //プレハブ生成
        GameObject SoundWaves = (GameObject)Resources.Load("SoundWaves");
        SoundWaves = Instantiate(SoundWaves, tf.transform.position, Quaternion.Euler(90,0,0));
    }

    public override void CustumUpdate()
    {
        //if (isGround)
        //{
        //    if (Input.GetKeyDown(jumpKey)) // キー入力判定
        //    {
        //        JumpKeyDown = true;
        //    }
        //}
    }

    public override void CustumFixed()
    {
        //アニメーション
        if (!isGround)
        {
            if (Player.instance.GetAnim().Anim.AnimationName != "recoder/recoder_jump_attack")
            {
                float NowTime = m_TrackEntry.TrackTime;
                //空中
                m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "recoder/recoder_jump_attack", true);
                m_TrackEntry.TrackTime = NowTime;
            }

        }
        else
        {
            if (Mathf.Abs(SelfVel.x /*+ OtherVel.x*/) >= STAND_SPEED)
            {

                if (Player.instance.GetAnim().Anim.AnimationName != "recoder/recoder_run_attack")
                {
                    float NowTime = m_TrackEntry.TrackTime;
                    //走り
                    m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "recoder/recoder_run_attack", true);
                    m_TrackEntry.TrackTime = NowTime;
                }

            }
            else
            {
                if (Player.instance.GetAnim().Anim.AnimationName != "recoder/recoder_idel_attack")
                {
                    float NowTime = m_TrackEntry.TrackTime;
                    //立ち
                    m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "recoder/recoder_idel_attack", true);

                    m_TrackEntry.TrackTime = NowTime;
                }

            }
        }


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
            //向き変更
            ChangeDirection();
            //ジャンプ処理
            Jump();
            //状態遷移
            //ChangeNextState();
            //浮遊
            AirBorneCheck(true);
            //速度反映
            MovePlayer();
            //フラグリセット
            //JumpKeyDown = false;
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
            //向き変更
            ChangeDirection();
            //状態遷移
            //ChangeNextState();
            //浮遊
            AirBorneCheck(true);
            //速度反映
            MovePlayer();
            //フラグリセット
            //JumpKeyDown = false;
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
