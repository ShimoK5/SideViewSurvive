using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : EnemyIF
{
    int IdleCnt = 0;
    enum FORMATION_STATE
    {
        IDLE,
        GO
    }

    FORMATION_STATE FormationState;

    float StartPosY;

    public EnemyFormation(EnemyIF oldEnemy)
    {
        CopyEnemy(oldEnemy);
        SelfVel = new Vector2(0, 0);
        StartPosY = tf.transform.position.y;    
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
        //移動
        //FlameCount++;
        //if (FlameCount > 120)
        //    FlameCount = 0;

        switch (FormationState)
        { 
            case FORMATION_STATE.IDLE:
                //カウント加算
                IdleCnt++;

                int Temp;
                //10以下なら
                if((IdleCnt % 40)<= 20 )
                {
                    //IdleCntをそのまま使う
                    Temp = (IdleCnt % 40);
                }
                else
                {
                    Temp = 20 * 2 - (IdleCnt % 40); 
                }

                //Y高さ移動
                tf.position = new Vector3(tf.position.x, StartPosY + Temp * 0.03f, tf.position.z);

                SelfVel.x = 0;
                SelfVel.y = 0;
                break;

            case FORMATION_STATE.GO:
                //Y高さ合わせ
                tf.position = new Vector3(tf.position.x, StartPosY, tf.position.z);
                SelfVel.x = Mathf.Max(SelfVel.x - MAX_RUN_SPEED * 2, -MAX_RUN_SPEED * 15);

                //SelfVel.x = -MAX_RUN_SPEED * 9;
                SelfVel.y = 0;
                break;
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

    public override void Go()
    {
        FormationState = FORMATION_STATE.GO;
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
