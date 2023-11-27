using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirCannon : PlayerIF
{
    public PlayerAirCannon(PlayerIF oldPlayer)
    {
        //if (Player.instance.GetAnim().Anim.AnimationName != "normal/idle")
            //Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/idle", true);
        CopyPlayer(oldPlayer);

        Player.instance.GetAnim().Anim.state.SetAnimation(0, "aircanon/aircannon_fly", true);

        //減速
        SelfVel.x *= ACTION_VEL_MULTI;
        //ジャンプ処理
        SelfVel.y = 50.0f * MultiplyNum;
        OtherVel.y = 0;

        //無敵
        ActionInvisible = true;

        //プレハブ生成
        GameObject AirCannonAir = (GameObject)Resources.Load("AirCannonAir");
        AirCannonAir = Instantiate(AirCannonAir, tf.transform.position, tf.transform.rotation);
        AirCannonAir.GetComponent<AirCannonAir>().Velocity = new Vector3(0, -0.5f, 0);

        //GameObject GymClothesSmall = (GameObject)Resources.Load("Umbrella");
        //GymClothesSmall = Instantiate(GymClothesSmall, tf.transform.position, Quaternion.Euler(-90, tf.transform.eulerAngles.y - 90, 0));
        //GymClothesSmall.GetComponent<Umbrella>().InitSetPosition();
    }
    ~PlayerAirCannon()
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
            //Jump();
            //状態遷移
            ChangeNextState();
            //浮遊
            AirBorneCheck(false);
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
            //横移動       上昇中の横移動を減らす
            MoveX(MAX_AIR_SPEED * ACTION_VEL_MULTI * 0.03f, ADD_AIR_SPEED * ACTION_VEL_MULTI * 0.03f);
            //向き変更
            ChangeDirection();
            //状態遷移
            ChangeNextState();
            //浮遊
            AirBorneCheck(false);
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
        if(SelfVel.y < 0)
        {
            NextPlayerState = PLAYER_STATE.AIR;
        }

    }

    protected override void Fall()
    {
        SelfVel.y -= GLAVITY * 3;
    }
}
