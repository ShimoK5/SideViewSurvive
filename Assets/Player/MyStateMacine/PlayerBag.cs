using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : PlayerIF
{
    int AfterGapFlame = 7;
    int GroundFlameCount = 0;
    int AirFlameCnt = 0;

    bool PrefabOnce = true;

    public PlayerBag(PlayerIF oldPlayer)
    {
        //if (Player.instance.GetAnim().Anim.AnimationName != "normal/idle")
            Player.instance.GetAnim().Anim.state.SetAnimation(0, "normal/idle", true);
        CopyPlayer(oldPlayer);
        //横移動消し
        SelfVel.x = 0;
        OtherVel.x = 0;
        //下降
        SelfVel.y = -30.0f * MultiplyNum;
        OtherVel.y = 0;

        //無敵
        ActionInvisible = true;

        GroundFlameCount = 0;

        


        //GameObject GymClothesSmall = (GameObject)Resources.Load("Umbrella");
        //GymClothesSmall = Instantiate(GymClothesSmall, tf.transform.position, Quaternion.Euler(-90, tf.transform.eulerAngles.y - 90, 0));
        //GymClothesSmall.GetComponent<Umbrella>().InitSetPosition();
    }
    ~PlayerBag()
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
            if(PrefabOnce)
            {
                CreatePrefab();
                PrefabOnce = false;
            }

            GroundFlameCount++;

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
            AirFlameCnt++;

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
        if (GroundFlameCount >= AfterGapFlame)
        {
            NextPlayerState = PLAYER_STATE.STAND;
        }

    }

    void CreatePrefab()
    {
        //プレハブ生成
        GameObject BagShockWave = (GameObject)Resources.Load("BagShockWave");
        BagShockWave = Instantiate(BagShockWave, tf.transform.position, tf.transform.rotation);
        //値渡し
        BagShockWave.GetComponent<BagShockWave>().SetParam((float)AirFlameCnt / RhythmManager.Instance.BeatTempo);
        //vel渡し
        BagShockWave.GetComponent<BagShockWave>().Velocity = new Vector3(0.4f, 0, 0);
        //高さ調整
        BagShockWave.transform.position = new Vector3(BagShockWave.transform.position.x,
            BagShockWave.transform.position.y + (BagShockWave.GetComponent<BagShockWave>().GetSize() - 2) * 0.5f,
            BagShockWave.transform.position.z);

        //プレハブ生成
        GameObject BagShockWave2 = (GameObject)Resources.Load("BagShockWave");
        BagShockWave2 = Instantiate(BagShockWave2, tf.transform.position, tf.transform.rotation);
        //値渡し
        BagShockWave2.GetComponent<BagShockWave>().SetParam((float)AirFlameCnt / RhythmManager.Instance.BeatTempo);
        //vel渡し
        BagShockWave2.GetComponent<BagShockWave>().Velocity = new Vector3(-0.4f, 0, 0);
        //高さ調整
        BagShockWave2.transform.position = new Vector3(BagShockWave2.transform.position.x,
            BagShockWave2.transform.position.y + (BagShockWave2.GetComponent<BagShockWave>().GetSize() - 2) * 0.5f,
            BagShockWave2.transform.position.z);
    }
}
