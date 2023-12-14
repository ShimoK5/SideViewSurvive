using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class PlayerSacrifice : PlayerIF
{
    TrackEntry m_TrackEntry;
    int FCnt = 0;
    public PlayerSacrifice(PlayerIF oldPlayer)
    {
        //if (Player.instance.GetAnim().Anim.AnimationName != "bear/throw")
            //Player.instance.GetAnim().Anim.state.SetAnimation(0, "bear/throw", true);
        CopyPlayer(oldPlayer);
        FCnt = 0;
        //アニメーション
        if (!isGround)
        {
            //
            //空中
            m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "bear/jump_throw", true);
        }
        else
        {
            if (Mathf.Abs(SelfVel.x /*+ OtherVel.x*/) >= STAND_SPEED)
            {
                //走り
                m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "bear/run_throw", true);
            }
            else
            {
                //立ち
                m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "bear/throw", true);

            }
        }

        //減速
        SelfVel.x *= ACTION_VEL_MULTI;
        //プレハブ生成
        GameObject Sacrifice = (GameObject)Resources.Load("Sacrifice");
        Sacrifice = Instantiate(Sacrifice, tf.transform.position,Quaternion.Euler(tf.transform.localEulerAngles));
        Sacrifice.GetComponent<Sacrifice>().SetVel(new Vector2(tf.forward.x * 0.07f, 0.4f));
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
        FCnt++;
        //アニメーション
        if (!isGround)
        {
            if (Player.instance.GetAnim().Anim.AnimationName != "bear/jump_throw")
            {
                float NowTime = m_TrackEntry.TrackTime;
                //空中
                m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "bear/jump_throw", true);
                m_TrackEntry.TrackTime = NowTime;
            }
                
        }
        else
        {
            if (Mathf.Abs(SelfVel.x /*+ OtherVel.x*/) >= STAND_SPEED)
            {

                if (Player.instance.GetAnim().Anim.AnimationName != "bear/run_throw")
                {
                    float NowTime = m_TrackEntry.TrackTime;
                    //走り
                    m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "bear/run_throw", true);
                    m_TrackEntry.TrackTime = NowTime;
                }
                
            }
            else
            {
                if (Player.instance.GetAnim().Anim.AnimationName != "bear/throw")
                {
                    float NowTime = m_TrackEntry.TrackTime;
                    //立ち
                    m_TrackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "bear/throw", true);

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
            ChangeNextState();
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
            ChangeNextState();
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
    protected override void ChangeNextState()
    {
        if (FCnt >= 30)
        {
            if (!isGround)
            {
                NextPlayerState = PLAYER_STATE.AIR;
            }
            else
            {
                if (Mathf.Abs(SelfVel.x /*+ OtherVel.x*/) >= STAND_SPEED && NextPlayerState == PLAYER_STATE.STAND)
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
}
