using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPress : EnemyIF
{
    int MoveFlameCount = 0; //頭上調整時間
    int PressDelay = -1; //落下前動作待機時間(-1は非稼働)
    int Delay = -1; //動作待機時間(-1は非稼働)
    bool Press = false; //落下許可フラグ
    bool Return = false; //再浮遊フラグ
    Vector3 StartPos; //初期座標(戻る高さの取得

    public EnemyPress(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
        StartPos = tf.transform.position;
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

        //
        if(!Press && !Return && Delay == -1 && PressDelay == -1)
        MoveFlameCount++;

        //地面接触時の停止時間カウントダウン
        if (Delay > 0)
            Delay--;

        //300fたったら頭上移動を停止させる
        if (MoveFlameCount > 200)
        {
            MoveFlameCount = 0;
            PressDelay = 30;
        }
        else if(PressDelay >= 0) //動作停止時間が稼働中の場合
        {
            //待機カウント終了
            if(PressDelay == 0)
                Press = true;

            PressDelay--;
            SelfVel.x = 0.0f;
        }
        
        //頭上位置調整のための移動制御(調整時間がある場合の間)
        if(MoveFlameCount != 0)
        {
            Vector3 playerPos = Player.instance.transform.position;
            float sign = Mathf.Sign(playerPos.x - tf.position.x);
            SelfVel.x = 8 * (1.0f / 65) * sign;
        }

        //落下許可が下りているとき落下
        if(Press)
        {
            //停止カウンターを非稼働に
            PressDelay = -1;

            Fall();
            SelfVel.y = -MAX_RUN_SPEED * 5.0f;
        }

        //地面についたら移動を止めて一定時間停止
        if (isGround && Press)
        {
            SelfVel.y = 0.0f;
            Delay = 5;
            Press = false;
        }

        //停止カウンター0の場合、浮遊を開始
        if(Delay == 0)
        {
            Return = true;

            //停止カウンターを非稼働に
            Delay = -1;
        }

        //浮遊命令が出ているとき最初の高さまで浮遊
        if (Return)
        {
            SelfVel.y = MAX_RUN_SPEED;
            if (tf.position.y > StartPos.y)
            {
                Return = false;
            }
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
