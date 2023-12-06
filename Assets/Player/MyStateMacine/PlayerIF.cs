﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;




public class PlayerIF : PawnIF
{
    //定数
    static public KeyCode jumpKey = KeyCode.Space; //ジャンプキー
    static protected float MultiplyNum = 1.0f / 60;
    static protected float STAND_SPEED = 0.4f * MultiplyNum;      //これ以下のスピードならRunからStand
    static protected float MAX_RUN_SPEED = 8 * MultiplyNum;       //地上最大速度
    static protected float ADD_RUN_SPEED = 2.5f * MultiplyNum;    //地上加速度
    static protected float MAX_AIR_SPEED = 4 * MultiplyNum;       //空中最大速度
    //static protected float MAX_AIR_SPEED = 6;       //空中最大速度
    static protected float ADD_AIR_SPEED = 1.25f * MultiplyNum;       //空中最大速度
    static protected float GROUND_VEL_MULTI = 0.81f;    //地上減速（摩擦）
    static protected float AIR_VEL_MULTI = 0.998f;      //空中減速（空気抵抗）
    static protected float GLAVITY = 0.68f * MultiplyNum;       //重力
    static protected float ACTION_VEL_MULTI = 0.8f;     //アクション中の減速率
    public float KNOCK_BACK_POWER = 20 * MultiplyNum;   //ノックバックの強さ
    static protected int MAX_AIRBORNE_FLAME = 60;          //滞空時間

    public  PLAYER_STATE PlayerState { get; set; } = PLAYER_STATE.STAND;
    public PLAYER_STATE NextPlayerState { get; set; } = PLAYER_STATE.STAND;

   
    public bool isGround = true;   //地面に触れているかのフラグ
    public bool OldisGround = true;
    public Vector2 SelfVel;         //自己速度
    public Vector2 OtherVel;        //外部速度
    public Vector2 AllVel;          //合算速度
    //protected bool JumpKeyDown = false; //ジャンプキーを押しているかどうか            
    static AfterImage AfterImageInstanse = new AfterImage();

    public bool ActionInvisible;    //アクション無敵フラグ
    public int AirBorneFlame;       //滞空したフレーム数
    public bool AlreadyAirBorne = false;    //既に浮遊したかどうかのフラグ
    public bool NowAirBorne = false;        //浮遊中かどうかのフラグ
    
    //コピー関数
    //全ての変数をコピーする
    protected void CopyPlayer(PlayerIF oldPlayer)
    {
        PlayerState = oldPlayer.PlayerState;
        NextPlayerState = oldPlayer.NextPlayerState;

        isGround = oldPlayer.isGround;
        OldisGround = oldPlayer.OldisGround;
        SelfVel = oldPlayer.SelfVel;
        OtherVel = oldPlayer.OtherVel;
        //JumpKeyDown = oldPlayer.JumpKeyDown;
        tf = oldPlayer.tf;
        Size = oldPlayer.Size;
        AirBorneFlame = oldPlayer.AirBorneFlame;
        AlreadyAirBorne = oldPlayer.AlreadyAirBorne;
        NowAirBorne = oldPlayer.NowAirBorne;
    }

    //PlayerIF(PlayerIF oldPlayer)
    //{
    //    // 親クラスのプロパティ情報を一気に取得して使用する。
    //    List<PropertyInfo> props = oldPlayer
    //        .GetType()
    //        .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
    //        .ToList();

    //    props.ForEach(prop =>
    //    {
    //        var propValue = prop.GetValue(oldPlayer);
    //        typeof(PlayerIF).GetProperty(prop.Name).SetValue(this, propValue);
    //    });
    //}

    public void CustumStart()
    {
        tf = Player.instance.GetComponent<Transform>();
        tf.transform.localEulerAngles = new Vector3(0, 90, 0);
        Size = tf.transform.GetComponent<MeshRenderer>().GetComponent<MeshRenderer>().bounds.size;
        //JumpKeyDown = false;
        ActionInvisible = false;
        AirBorneFlame = 0;
        //PlayerAnim.instans.Anim.SetInteger("AnimStateCnt", 1);
    }
    public virtual void CustumUpdate()//仮想関数
    {

    }
    public virtual void CustumFixed()//仮想関数
    {

    }

    //CustumFixedの共通の中身をまとめた関数
    protected void FixedCommon()
    {
        //残像処理
        //AfterImage();
        //移動床追従
        //FollowMoveBlock();

        if(isGround != OldisGround)
        {
            if(isGround)
            {
                //プレハブ生成
                GameObject Effect = (GameObject)Resources.Load("Prefabs/vfx_PlayerLanding");
                Effect = Instantiate(Effect, tf.transform.position, Quaternion.Euler(0,-90,0));
            }
            //崖から降りた時に出ちゃうから中止
            //else
            //{
                
                //プレハブ生成
                //GameObject Effect = (GameObject)Resources.Load("Prefabs/vfx_PlayerJumping");
                //Effect = Instantiate(Effect, tf.transform.position, Quaternion.Euler(0, -90, 0));
            //}
        }

        OldisGround = isGround;

        //ヒップドロップの軌跡しまう
        if(Player.instance.TempState != PLAYER_STATE.BAG)
        {
            Player.instance.GetComponent<TrailRenderer>().enabled = false;
        }

    }

    //速度反映関数
    protected void MovePlayer()
    {
        AllVel = SelfVel + OtherVel;
        tf.transform.position += new Vector3(AllVel.x, AllVel.y ,0);
    }

    //横移動   
    protected void MoveX(float maxSpeed , float addSpeed)
    {
        if(InputManager_FU.instanse)
        {
            //移動処理
            if (InputManager_FU.instanse.GetKey(Key.Right)) // キー入力判定
            {

                if (SelfVel.x <= maxSpeed)
                {
                    SelfVel.x = Mathf.Min(maxSpeed, SelfVel.x + addSpeed);
                }
            }
            if (InputManager_FU.instanse.GetKey(Key.Left)) // キー入力判定
            {

                if (SelfVel.x >= -maxSpeed)
                {
                    SelfVel.x = Mathf.Max(-maxSpeed, SelfVel.x - addSpeed);
                }
            }
        }
    }

    //向き変更
    protected void ChangeDirection()
    {
        if(SelfVel.x > 0)
        {
            tf.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (SelfVel.x < 0)
        {
            tf.transform.localEulerAngles = new Vector3(0, -90, 0);
        }

    }


    //横速度減速
    protected void SlowDown(float selfVelMulti, float otherVelMulti)
    {
        SelfVel.x *= selfVelMulti;
        OtherVel.x *= otherVelMulti;
    }
    //自由落下
    protected virtual void Fall()
    {
        SelfVel.y -= GLAVITY;
    }
    //ジャンプ
    protected void Jump()
    {
        if(InputManager_FU.instanse)
        {
            if (InputManager_FU.instanse.GetKeyTrigger(Key.A) && isGround) // キー入力判定
            {
                //NextPlayerState = PLAYER_STATE.AIR;
                SelfVel.y = 20.0f * MultiplyNum;
                GameObject Effect = (GameObject)Resources.Load("Prefabs/vfx_PlayerJumping");
                Effect = Instantiate(Effect, tf.transform.position, Quaternion.Euler(0, -90, 0));

            }
        }
        
    }
    //状態遷移
    protected virtual void ChangeNextState() 
    {
        Debug.Log("オーバーライドしていない関数が呼ばれた");
    }

    //床に合わせた座標移動
    protected void FollowMoveBlock()
    {
        if(StandBlock)
        {
            tf.position += (StandBlock.transform.position - StandBlock.OldPos);
            Debug.Log("Stand");
        }
        else
        {
            Debug.Log("Fly");

        }
    }

    //滞空
    protected void AirBorneCheck(bool canAirborne)//引数は滞空可能かどうか
    {
        //入力instanceなければ
        if (!InputManager_FU.instanse)
        {
            return;
        }
        //地上なら
        if (isGround )
        {
            ResetAirborneData();
            return;
        }
        //引数滞在不可なら
        if (!canAirborne)
        {
            //直前まで浮遊していたら
            if (NowAirBorne)
            {
                //既に浮遊したフラグをオン
                AlreadyAirBorne = true;
            }

            //現在の浮遊状態を保存
            NowAirBorne = false;

            return;
        }


        //入力ありかつ下降中かつまだ浮遊してないかつ制限時間を超えていなければ
        if (InputManager_FU.instanse.GetKey(Key.A) && 
            SelfVel.y <= 0 &&
            !AlreadyAirBorne &&
            AirBorneFlame <= MAX_AIRBORNE_FLAME) 
        {
            //下降を止める
            SelfVel.y = 0;
            OtherVel.y = 0;
            //カウントを加算する
            AirBorneFlame++;
            //現在の浮遊状態を保存
            NowAirBorne = true;

            //GameObject Effect = (GameObject)Resources.Load("Prefabs/vfx_PlayerJumping");
            //Effect = Instantiate(Effect, tf.transform.position, Quaternion.Euler(0, -90, 0));

        }
        else
        {
            //直前まで浮遊していたら
            if (NowAirBorne)
            {
                //既に浮遊したフラグをオン
                AlreadyAirBorne = true;
            }
            //現在の浮遊状態を保存
            NowAirBorne = false;
        }
        
        
    }

    void ResetAirborneData()
    {
        NowAirBorne = false;
        AlreadyAirBorne = false;
        AirBorneFlame = 0;
    }

    public override void HitUnder(Block block)
    {
        StandBlock = block;
        isGround = true;
        float YPos = block.transform.position.y + (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        
        if(SelfVel.y + OtherVel.y <= 0)
        {
            SelfVel.y = 0;
            OtherVel.y = 0;

        }
        //SelfVel.y = Mathf.Max(0.0f , SelfVel.y) ;
        //OtherVel.y = Mathf.Max(0.0f, OtherVel.y);
    }
    public override void HitTop(Block block) 
    {
        float YPos = block.transform.position.y - (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        
        if (SelfVel.y + OtherVel.y >= 0)
        {
            SelfVel.y = 0;
            OtherVel.y = 0;
        }

        //SelfVel.y   = Mathf.Min(0.0f , SelfVel.y) ;
        //OtherVel.y  = Mathf.Min(0.0f, OtherVel.y);
    }
    public override void HitRight(Block block) 
    {
        float XPos = block.transform.position.x - (block.Size.x + Size.x) / 2;
        tf.transform.position = new Vector3(XPos, tf.transform.position.y, tf.transform.position.z);

        if (SelfVel.x + OtherVel.x >= 0)
        {
            SelfVel.x = 0;
            OtherVel.x = 0;
        }

        //SelfVel.x   = Mathf.Min(0.0f , SelfVel.x) ;
        //OtherVel.x  = Mathf.Min(0.0f, OtherVel.x);
    }
    public override void HitLeft(Block block) 
    {
        float XPos = block.transform.position.x + (block.Size.x + Size.x) / 2;
        tf.transform.position = new Vector3(XPos, tf.transform.position.y, tf.transform.position.z);

        if (SelfVel.x + OtherVel.x <= 0)
        {
            SelfVel.x = 0;
            OtherVel.x = 0;
        }

        //SelfVel.x = Mathf.Max(0.0f , SelfVel.x) ;
        //OtherVel.x = Mathf.Max(0.0f, OtherVel.x);
    }
    public override void NonHitUnder() 
    {
        isGround = false;
        StandBlock = null;
    }

    public override void Umore()
    {
        if ( GameStateManager.instance.GameState == GAME_STATE.Game)
        {
            //GameStateManager.instance.GameState = GAME_STATE.DeadPlayerStop;
            Player.instance.HitPoint = 0;
        }
    }

#if false
    //全方位とのあたり判定
    protected void HitAround()
    {
        HitGround();
        HitUp();
        HitRight();
        HitLeft();
    }


    protected void HitGround()
    {
        isGround = false; //取り敢えず地面に接触していない事にする

        //下に向かっていれば
        if (SelfVel.y + OtherVel.y < 0.0f)
        {
            //繰り返し
            foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.15f, 0.5f, 0.5f),
            -rb.transform.up, Quaternion.identity, 100f))
            {
                if(hit.collider.gameObject.tag == "Platform_Default" /*||
                    hit.collider.gameObject.tag == "Platform_TopOnly"*/)
                {
                    float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得
                    if (distance < (PlayerSize.y * 0.25f) * 1.05f)
                    {
                        isGround = true; //地面に接触している
                        SelfVel.y = 0.0f;
                        OtherVel.y = 0.0f;
                        //地面にプレイヤーをくっつける     これをするとジャンプできない
                        //rb.transform.position = new Vector3(rb.transform.position.x, hit.point.y + PlayerSize.y / 2, rb.transform.position.z);
                    }
                }
            }
        }
            
    }

    protected void HitUp()
    {
        //繰り返し
        foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.15f, 0.5f, 0.5f),
            rb.transform.up, Quaternion.identity, 100f))
        {
            if (hit.collider.gameObject.tag == "Platform_Default")
            {
                float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得

                if (distance < (PlayerSize.y * 0.25f) * 1.2f)
                {

                    if (SelfVel.y + OtherVel.y > 0.0f)
                    {
                        SelfVel.y = 0.0f;
                        OtherVel.y = 0.0f;
                    }
                }
            }
        }
    }

    protected void HitRight()
    {
        //繰り返し
        foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.4f, 0.5f, 0.5f),
             new Vector3(1, 0, 0), Quaternion.identity, 100f))
        {
            if (hit.collider.gameObject.tag == "Platform_Default")
            {
                float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得

                if (distance < 0.1f)
                {
                    //壁に向かっていれば                 空中なら
                    if (SelfVel.x + OtherVel.x > 0.0f && isGround == false)
                    {
#if true
                        SelfVel.x = 0.0f;
                        OtherVel.x = 0.0f;
#endif
                    }
                }
                //Debug.Log(distance);
            }
        }
    }
    protected void HitLeft()
    {
        //繰り返し
        foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.4f, 0.5f, 0.5f),
             new Vector3(-1, 0, 0), Quaternion.identity, 100f))
        {
            if (hit.collider.gameObject.tag == "Platform_Default")
            {
                float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得

                if (distance < 0.1f)
                {
                    //壁に向かっていれば                 空中なら
                    if (SelfVel.x + OtherVel.x < 0.0f && isGround == false)
                    {
#if true
                        SelfVel.x = 0.0f;
                        OtherVel.x = 0.0f;
#endif
                    }

                }
                //Debug.Log(distance);
            }
        }
    }
#endif

    //過去情報h保存
    protected void KeepOld()
    {
        OldPos = tf.transform.position;
    }

    //残像エフェクト
    public void AfterImage()
    {

        AfterImageInstanse.ImageUpdate(tf, AllVel);
    }

    public float GetStandSpeed()
    {
        return STAND_SPEED;
    }

}

public class AfterImage : MonoBehaviour
{
    float m_Time = 0;//経過時間
    float RemitTime = 0;//これを越えたらエフェクトを出すという時間
    GameObject myPrefab = (GameObject)Resources.Load("CubeParent");//プレハブをGameObject型で取得
    float TimeRenge = 1.5f / 50;//エフェクトが出る頻度

    public AfterImage()
    {
        m_Time = 0;
        RemitTime = 1000;
    }
    public void ImageUpdate(Transform tf , Vector3 vel)
    {
        //停止時
        if(vel.magnitude <= 0.012f)
        {
            //カウントタイムリセット
            m_Time = 0;
        }
        else
        {
            //速度に応じてリミットを変更
            RemitTime = TimeRenge / vel.magnitude;
            //Debug.Log(RemitTime);
            //タイム加算
            m_Time += Time.deltaTime;
            if (m_Time > RemitTime)
            {
                m_Time = 0;
                // プレハブを元に、インスタンスを生成、
                GameObject Obj = Instantiate(myPrefab,tf.position + new Vector3(0,0,0.6f), Quaternion.identity);
            }
        }
        
    }
}

