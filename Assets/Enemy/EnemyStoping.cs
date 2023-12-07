using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class EnemyStoping : EnemyIF
{
    private int FlameCount = 0;
    private float Radian;

    public EnemyStoping(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
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
        //Fall();
        //縦移動
        //FlameCount++;
        //if (FlameCount > 120)
        //    FlameCount = 0;

        //if (FlameCount < 60)
        //    SelfVel.y = -MAX_RUN_SPEED;
        //else if (FlameCount > 60)
        //    SelfVel.y = MAX_RUN_SPEED;

        Sacrifice[] SacrificeArray = GameObject.FindObjectsOfType<Sacrifice>();

        if (SacrificeArray.Length != 0)
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
            SelfVel.x = Mathf.Cos(Radian) * MAX_RUN_SPEED * 1.3f;
            SelfVel.y = Mathf.Sin(Radian) * MAX_RUN_SPEED * 1.3f;
        }

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
        //isGround = true;
        //StandBlock = block;
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