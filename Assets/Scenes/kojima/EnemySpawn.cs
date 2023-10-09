using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject SkySpownZone;
    private int FlameCount = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        FlameCount++;

        if (FlameCount > 30)
            FlameCount = 0;

        if (FlameCount == 30)
        {
            GameObject Enemy = (GameObject)Resources.Load("EnemyZigzag");
            
            Vector3 Size = transform.GetComponent<MeshRenderer>().bounds.size;
            float RandomPosX = Random.Range(-Size.x / 2, Size.x / 2);
            float RandomPosY = Random.Range(-Size.y / 2, Size.y / 2);
            Vector3 SpownPos = transform.position + new Vector3(RandomPosX, RandomPosY, 0.0f);

            Enemy = Instantiate(Enemy, SpownPos, Quaternion.Euler(0, 0, 0));
            
        }
       
          
    }

    //要件定義(現時点)
    //スポーンゾーン内のランダムな場所にエネミーをスポーン
    //上空のスポーンゾーンからは特定のエネミーのみをスポーン
    //ハビタブルゾーン内の総エネミー数(=現存する非設置型のエネミー数)が一定以下(目安15)の時にスポーン処理
    //総エネミー数が常に15体になるように出現させる
    //スポーン周期は30f
}
