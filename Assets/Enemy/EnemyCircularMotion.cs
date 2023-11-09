using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class EnemyCircularMotion : EnemyIF
{
    public float Radius = 5.0f;
    private int FlameCount = 0;
    private static Vector3 CenterCoordinates;//中心点
    private Vector2 TargetLocation;

    public EnemyCircularMotion(EnemyIF oldEnemy)
    {
        //EnemyAnim.instans.Anim.SetTrigger("Run");
        
        CopyEnemy(oldEnemy);
       
        CenterCoordinates = tf.transform.position;
        tf.transform.position = new Vector3(tf.transform.position.x, tf.transform.position.y, tf.transform.position.z);
    }

    void Start()
    {
       
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
        FlameCount++;

        TargetLocation.x = Radius * Mathf.Cos(FlameCount * Mathf.PI / 180.0f);
        TargetLocation.y = Radius * Mathf.Sin(FlameCount * Mathf.PI / 180.0f);

        tf.transform.position = new Vector3(CenterCoordinates.x + TargetLocation.x, CenterCoordinates.y + TargetLocation.y, tf.transform.position.z);


        if (FlameCount > 360)
            FlameCount = 0;

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
