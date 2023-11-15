using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class EnemyDead : EnemyIF
{
    int FCnt = 0;

    public EnemyDead(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        CopyEnemy(oldEnemy);
        FCnt = 0;

        //仮
        //tf.GetComponent<Renderer>().material.color = Color.grey;

        //速度反映
        OtherVel = Vector2.zero;
        SelfVel = DeadVector * 0.8f;
    }

    public override void CustumUpdate()
    {

    }

    public override void CustumFixed()
    {
        FCnt++;

        if(FCnt > 7)
        {
            GameObject myPrefab;//プレハブをGameObject型で取得
            myPrefab = (GameObject)Resources.Load("Prefabs/vfx_EnemyDeathMagenta");//プレハブをGameObject型で取得
            GameObject Obj = Instantiate(myPrefab, tf.position, Quaternion.identity);

            //マネージャーに加算を送るフラグtrueなら
            if(DeadCntFlag)
                EnemyKillCountManager.Instance.DestroyCountUp();

            Destroy(tf.gameObject);

        }
        ////過去情報保存
        KeepOld();
        ////勢い減少 
        //SlowDown(GROUND_VEL_MULTI, GROUND_VEL_MULTI);
        ////自由落下
        ////Fall();
        ////横移動
        //MoveX(MAX_RUN_SPEED, ADD_RUN_SPEED);
        ////ジャンプ処理
        ////Jump();
        ////状態遷移
        //ChangeNextState();
        ////速度反映

        SelfVel *= 0.95f;

        MoveEnemy();
        ////フラグリセット
        ////JumpKeyDown = false;
        ////共通更新
        //FixedCommon();
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
