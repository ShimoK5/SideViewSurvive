using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine;

public class PlayerDead : PlayerIF
{
    public PlayerDead(PlayerIF oldPlayer)
    {
        if (Player.instance.GetAnim().Anim.AnimationName != "normal/idle")
            Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/idle", true);
        CopyPlayer(oldPlayer);
        //横移動消し
        SelfVel.x = 0;
        OtherVel.x = 0;
        //下降
        SelfVel.y = 0;
        OtherVel.y = 0;

        //GameObject GymClothesSmall = (GameObject)Resources.Load("Umbrella");
        //GymClothesSmall = Instantiate(GymClothesSmall, tf.transform.position, Quaternion.Euler(-90, tf.transform.eulerAngles.y - 90, 0));
        //GymClothesSmall.GetComponent<Umbrella>().InitSetPosition();
    }
    ~PlayerDead()
    {
        
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
        //ゴールアニメーション開始
        if (Player.instance.GetAnim().Anim.AnimationName != "normal/walking")
        {
            TrackEntry trackEntry = Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/walking", true);
            trackEntry.Complete += SceneChange;
        }

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
            //ChangeDirection();
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
            //ChangeDirection();
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
        

    }

    public void SceneChange(TrackEntry trackEntry)
    {
        SceneChangeManager.instance.SceneTransition(SceneManager.GetActiveScene().name);
        Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/idle", true);
    }

}
