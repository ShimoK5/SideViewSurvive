using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPress : EnemyIF
{
    enum PressState
    {
        search,
        stop,
        press,
        up,
        none
    }

    int MoveFlameCount = 0; //頭上調整時間
    int Delay = -1; //動作待機時間(-1は非稼働)
    bool Press = false; //落下許可フラグ
    bool Return = false; //再浮遊フラグ
    Vector3 StartPos; //初期座標(戻る高さの取得
    PressState State; //現在のステート
    PressState PreviousState; //1f前のステート
    PressState OldState; //ステート変更前のステート

    public EnemyPress(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
        StartPos = tf.transform.position;
        State = PressState.search;
        PreviousState = OldState = PressState.none;
    }

    public override void CustumUpdate()
    {

    }

    public override void CustumFixed()
    {
        //過去情報保存
        KeepOld();

        if(PreviousState != State)
        {
            OldState = PreviousState;
            PreviousState = State;
        }
        //Debug.Log(State);

        //勢い減少 
        //SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
        //自由落下
        //Fall();

        switch (State)
        {
            case PressState.search:
                {
                    // Sacrifice クラス型の配列に、シーン上すべてのSacrificeをぶち込む
                    Sacrifice[] SacrificeArray = GameObject.FindObjectsOfType<Sacrifice>();

                    if (SacrificeArray.Length == 0)
                    {
                        Vector3 playerPos = Player.instance.transform.position;
                        float sign = Mathf.Sign(playerPos.x - tf.position.x);

                        SelfVel.y = 0.0f;
                        SelfVel.x = 8 * (1.0f / 65) * sign;

                        if (playerPos.x - tf.position.x < SelfVel.x)
                            SelfVel.x = playerPos.x - tf.position.x;
                    }
                    else 
                    {
                        //最短距離保存変数。とりあえずクッソでかい値いれとく
                        float ShortestDistance = 1000000.0f;
                        int NeerIndex = 0;

                        // for回す
                        for (int i = 0; i < SacrificeArray.Length; i++)
                        {
                            float DistanceX = SacrificeArray[i].transform.position.x - tf.transform.position.x;
                            float DistanceY = SacrificeArray[i].transform.position.y - tf.transform.position.y;

                            float Distance = DistanceX * DistanceX + DistanceY * DistanceY;

                            if (Distance < ShortestDistance)
                            {
                                ShortestDistance = Distance;
                                NeerIndex = i;
                            }

                            float sign = Mathf.Sign(SacrificeArray[i].transform.position.x - tf.position.x);

                            SelfVel.y = 0.0f;
                            SelfVel.x = 8 * (1.0f / 65) * sign;

                            if (SacrificeArray[i].transform.position.x - tf.position.x < SelfVel.x)
                                SelfVel.x = SacrificeArray[i].transform.position.x - tf.position.x;
                        }
                    }

                    if (MoveFlameCount > 200)
                    {
                        State = PressState.stop;
                        MoveFlameCount = 0;
                        Delay = 30;
                    }

                    break;
                }

            case PressState.stop:
                {
                    SelfVel.x = 0.0f;
                    SelfVel.y = 0.0f;

                    if(Delay == 0)
                    {
                        if (OldState == PressState.search)
                        {
                            State = PressState.press;
                            Delay = 10;
                        }

                        if(OldState == PressState.press)
                            State = PressState.up;
                    }

                    break;
                }

            case PressState.press:
                {
                    
                    if (Delay > 0)
                        SelfVel.y = MAX_RUN_SPEED * 2;
                    else
                    {
                        SelfVel.y = -MAX_RUN_SPEED * 5.0f;
                        Fall();
                    }

                    if(isGround)
                    {
                        State = PressState.stop;
                        Delay = 30;
                    }
                    break;
                }

            case PressState.up:
                {
                    SelfVel.y = MAX_RUN_SPEED * 2.0f;

                    if (tf.position.y > StartPos.y)
                    {
                        SelfVel.y = 0.0f;
                        State = PressState.search;
                    }
                    break;
                }
        }

        if (State == PressState.search)
            MoveFlameCount++;

        if (Delay > 0)
            Delay--;

        //ジャンプ処理
        //Jump();
        //状態遷移
        ChangeNextState();
        //速度反映
        MoveEnemy();
        //フラグリセット
        //JumpKeyDown = false;
        //共通更新
        FixedCommon();
    }

    public override void HitUnder(Block block)
    {
        //Debug.Log("床");
        isGround = true;
        StandBlock = block;
        float YPos = block.transform.position.y + (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        SelfVel.y = 0.0f;
        OtherVel.y = 0.0f;
    }

    //状態遷移
    protected override void ChangeNextState()
    {
        //if (Mathf.Abs(SelfVel.x/* + OtherVel.x*/) < STAND_SPEED && NextEnemyState == ENEMY_STATE.RUN)
        //{
        //    NextEnemyState = ENEMY_STATE.STAND;
        //}
        //if (!isGround)
        //{
        //    NextEnemyState = ENEMY_STATE.AIR;
        //}
    }

}
