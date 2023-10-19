using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{
    [Header("滞在時間")]
    [SerializeField] int LifeCnt;

    int FCnt;//フレームカウント
    // Start is called before the first frame update
    void Start()
    {
        FCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position = Player.instance.transform.position;
        FCnt++;
        if (FCnt >= LifeCnt)
        {
            Destroy(gameObject);
        }

    }

    //はじき処理
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            //ベクトル計算
            Vector3 Obj_toE_Vec = other.transform.position - transform.position;
            Obj_toE_Vec = Obj_toE_Vec.normalized;

            other.GetComponent<Enemy>().SetOuterVel(
                Obj_toE_Vec.x * other.GetComponent<Enemy>().GetM_Enemy().GetMaxRunSpeed() * 1.5f ,
                Obj_toE_Vec.y * other.GetComponent<Enemy>().GetM_Enemy().GetMaxRunSpeed() * 1.5f);
        }
    }
}
