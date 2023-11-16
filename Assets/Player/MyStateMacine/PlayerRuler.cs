using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuler : PlayerIF
{
    int PlayerRulerFlame = 10;
    int FlameCount = 0;

    public PlayerRuler(PlayerIF oldPlayer)
    {
        //if (Player.instance.GetAnim().Anim.AnimationName != "normal/idle")
            Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/idle", true);
        CopyPlayer(oldPlayer);
        //横移動
        //右向き
        if (Player.instance.transform.rotation.y > 0)
        {
            SelfVel.x = MAX_RUN_SPEED * 7;
        }
        else
        {
            SelfVel.x = -MAX_RUN_SPEED * 7;
        }
            
        OtherVel.x = 0;
        //YVel無向化
        //SelfVel.y = 0;
        //OtherVel.y = 0;

        //無敵
        ActionInvisible = true;

        FlameCount = 0;

        //プレハブ生成
        //GameObject GymClothesSmall = (GameObject)Resources.Load("Umbrella");
        //GymClothesSmall = Instantiate(GymClothesSmall, tf.transform.position, Quaternion.Euler(-90, tf.transform.eulerAngles.y - 90, 0));
        //GymClothesSmall.GetComponent<Umbrella>().InitSetPosition();
    }
    ~PlayerRuler()
    {
        ActionInvisible = false;
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
        FlameCount++;

        if (isGround)
        {
            //過去情報保存
            KeepOld();
            //勢い減少
            //SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
            //自由落下
            Fall();
            //横移動
            //MoveX(MAX_RUN_SPEED * ACTION_VEL_MULTI, ADD_RUN_SPEED * ACTION_VEL_MULTI);
            //向き変更
            ChangeDirection();
            //ジャンプ処理
            //Jump();
            //状態遷移
            ChangeNextState();
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
            //SlowDown(AIR_VEL_MULTI, GROUND_VEL_MULTI);
            //自由落下
            Fall();
            //横移動       上昇中の横移動を減らす
            //MoveX(MAX_AIR_SPEED * ACTION_VEL_MULTI * 0.03f, ADD_AIR_SPEED * ACTION_VEL_MULTI * 0.03f);
            //向き変更
            ChangeDirection();
            //状態遷移
            ChangeNextState();
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
        if(FlameCount >= PlayerRulerFlame)
        {
            if (!isGround)
            {
                NextPlayerState = PLAYER_STATE.AIR;
                SelfVel.x = 0;
            }
            else
            {
                if (Mathf.Abs(SelfVel.x /*+ OtherVel.x*/) >= STAND_SPEED && NextPlayerState == PLAYER_STATE.STAND)
                {
                    NextPlayerState = PLAYER_STATE.RUN;
                    SelfVel.x = 0;
                }
                else
                {
                    NextPlayerState = PLAYER_STATE.STAND;
                    SelfVel.x = 0;
                }

            }
        }
    }
}
