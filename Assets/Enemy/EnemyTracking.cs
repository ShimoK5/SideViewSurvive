using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class EnemyTracking : EnemyIF
{
    private Vector3 PlayerPos;
    private int FlameCount = 60;
    private float SaveVelX;
    private float SaveVelY;
    private float Radian;

    public EnemyTracking(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
        Radian = Mathf.Atan2(PlayerPos.y - tf.transform.position.y, PlayerPos.x - tf.transform.position.x);
        SaveVelX = Mathf.Cos(Radian) * MAX_RUN_SPEED;
        SaveVelY = Mathf.Sin(Radian) * MAX_RUN_SPEED;
    }

    public override void CustumUpdate()
    {

    }

    public override void CustumFixed()
    {
        //過去情報保存
        KeepOld();
        //勢い減少 
        SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
        //自由落下
        Fall();

        // Sacrifice クラス型の配列に、シーン上すべてのSacrificeをぶち込む
        Sacrifice[] SacrificeArray = GameObject.FindObjectsOfType<Sacrifice>();

        FlameCount++;

        

        //移動
        if (SacrificeArray.Length == 0)//    ←「配列名.Length」で要素数取得
        {
            if (FlameCount == 60)
            {
                PlayerPos = Player.instance.transform.position;
                Radian = Mathf.Atan2(PlayerPos.y - tf.transform.position.y, PlayerPos.x - tf.transform.position.x);
                SaveVelX = Mathf.Cos(Radian) * MAX_RUN_SPEED;
                SaveVelY = Mathf.Sin(Radian) * MAX_RUN_SPEED;
               // FlameCount = 0;
            }
            else if(FlameCount > 100)
            {
                float RandomAddRot = Random.Range(-120.0f, 120.0f);
                Radian += RandomAddRot * Mathf.PI / 180.0f;
                SaveVelX = Mathf.Cos(Radian) * MAX_RUN_SPEED;
                SaveVelY = Mathf.Sin(Radian) * MAX_RUN_SPEED;
                FlameCount = 0;
            }
            else
            {
                SelfVel.x = SaveVelX;
                SelfVel.y = SaveVelY;
            }
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
            }
            //一番近い奴に追従するように書く
            //PlayerPos = Player.instance.transform.position;
            Radian = Mathf.Atan2(SacrificeArray[NeerIndex].transform.position.y - tf.transform.position.y,
                                        SacrificeArray[NeerIndex].transform.position.x - tf.transform.position.x);
            SelfVel.x = Mathf.Cos(Radian) * MAX_RUN_SPEED;
            SelfVel.y = Mathf.Sin(Radian) * MAX_RUN_SPEED;
        }

        //MoveX(MAX_RUN_SPEED, ADD_RUN_SPEED);
        //MoveY(MAX_RUN_SPEED, ADD_RUN_SPEED);
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
    public override void HitUnder(Block block)
    {
        //Debug.Log("床");
        //isGround = true;
        //StandBlock = block;
        float YPos = block.transform.position.y + (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        SelfVel.y = 0.0f;
        OtherVel.y = 0.0f;
    }
}

