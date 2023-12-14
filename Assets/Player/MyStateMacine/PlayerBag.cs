using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : PlayerIF
{
    int AfterGapFlame = 7;
    int GroundFlameCount = 0;
    int AirFlameCnt = 0;

    bool PrefabOnce = true;

    TrailRenderer tr;

    public PlayerBag(PlayerIF oldPlayer)
    {
        //if (Player.instance.GetAnim().Anim.AnimationName != "normal/idle")
            
        CopyPlayer(oldPlayer);

        Player.instance.GetAnim().Anim.state.SetAnimation(0, "hip_drow/hip_drow", true);

        tr = Player.instance.GetComponent<TrailRenderer>();
        //軌跡出現
        tr.enabled = true;

#if false
        //横移動消し
        SelfVel.x = 0;
        OtherVel.x = 0;
#else
        //減速
        SelfVel.x *= ACTION_VEL_MULTI;
#endif
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
        //軌跡しまう
        //Debug.Log("~PlayerBag");
        //Player.instance.GetComponent<TrailRenderer>().enabled = false;
        //Debug.Log(Player.instance.GetComponent<TrailRenderer>().enabled);

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
            SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
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
            //軌跡出現
            tr.enabled = true;

            AirFlameCnt++;

            //過去情報保存
            KeepOld();
            //勢い減少
            SlowDown(AIR_VEL_MULTI, GROUND_VEL_MULTI);
            //自由落下
            Fall();
            //横移動       下降中の横移動を減らす
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
        if (GroundFlameCount >= AfterGapFlame)
        {
            NextPlayerState = PLAYER_STATE.STAND;
        }

    }

    void CreatePrefab()
    {
        //エフェクトプレハブ生成
        GameObject Effect = (GameObject)Resources.Load("Prefabs/vfx_BagLanding");
        Effect = Instantiate(Effect, tf.transform.position + new Vector3(0, - Size.y * 0.5f + 0.02f, 0), tf.transform.rotation);

        //プレハブ生成
        GameObject BagShockWave = (GameObject)Resources.Load("BagShockWave");
        BagShockWave = Instantiate(BagShockWave, tf.transform.position, Quaternion.Euler(0, 90, 0));

        //vel渡し
        BagShockWave.GetComponent<BagShockWave>().Velocity = new Vector3(0.4f, 0, 0);
        //値渡し
        //BagShockWave.GetComponent<BagShockWave>().SetParam((float)AirFlameCnt / RhythmManager.Instance.BeatTempo);
        BagShockWave.GetComponent<BagShockWave>().SetParam((float)AirFlameCnt / 30);
        //高さ調整
        BagShockWave.transform.position = new Vector3(BagShockWave.transform.position.x,
            BagShockWave.transform.position.y + (BagShockWave.GetComponent<BagShockWave>().GetSize() - 2) * 0.5f,
            BagShockWave.transform.position.z);
        //横調整
        BagShockWave.transform.position = new Vector3(BagShockWave.transform.position.x + (BagShockWave.transform.lossyScale.x * 0.5f + Size.x * 0.5f),
            BagShockWave.transform.position.y ,
            BagShockWave.transform.position.z);

        //プレハブ生成
        GameObject BagShockWave2 = (GameObject)Resources.Load("BagShockWave");
        BagShockWave2 = Instantiate(BagShockWave2, tf.transform.position, Quaternion.Euler(0,-90,0));

        //vel渡し
        BagShockWave2.GetComponent<BagShockWave>().Velocity = new Vector3(-0.4f, 0, 0);
        //値渡し
        //BagShockWave2.GetComponent<BagShockWave>().SetParam((float)AirFlameCnt / RhythmManager.Instance.BeatTempo);
        BagShockWave2.GetComponent<BagShockWave>().SetParam((float)AirFlameCnt / 30);
        //高さ調整
        BagShockWave2.transform.position = new Vector3(BagShockWave2.transform.position.x,
            BagShockWave2.transform.position.y + (BagShockWave2.GetComponent<BagShockWave>().GetSize() - 2) * 0.5f,
            BagShockWave2.transform.position.z);
        //横調整
        BagShockWave.transform.position = new Vector3(BagShockWave.transform.position.x - (BagShockWave.transform.lossyScale.x * 0.5f + Size.x * 0.5f),
            BagShockWave.transform.position.y,
            BagShockWave.transform.position.z);
    }
}
