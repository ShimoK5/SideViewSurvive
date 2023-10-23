using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject SkySpownZone;
    private int FlameCount = 0;
    private const float ENEMY_SIZE = 0.99f;
    private Vector3 SpownPos;
    private GameObject Goal;

    [Header("ゴールとの非スポーン距離")]
    [SerializeField] float SpawnOffDistance = 20.0f; 
    
    [Header("エネミー最大存在数")]
    [SerializeField] int MaxEnemy = 20;

    // Start is called before the first frame update
    void Start()
    {
        Goal = GameObject.Find("GoalObj");
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
       

        if (FlameCount == 30 && GoalDistanceCheck() && OperatingEnemyCheck())
        {
            GameObject Enemy = (GameObject)Resources.Load("EnemyTracking");

            SpawnPosGenerate();

            Enemy = Instantiate(Enemy, SpownPos, Quaternion.Euler(0, 0, 0));
        }
    }

    bool Collision(Vector3 pos)
    {
        Block[] Blocks = GameObject.FindObjectsOfType<Block>();
        for (int i = 0; i < Blocks.Length; i++)
        {
            Vector3 b_Size = Blocks[i].transform.GetComponent<MeshRenderer>().bounds.size;
            Vector3 b_Pos = Blocks[i].transform.position;

            if (pos.y + ENEMY_SIZE > b_Pos.y - b_Size.y / 2 &&
            pos.y - ENEMY_SIZE < b_Pos.y + b_Size.y / 2 &&
            pos.x + ENEMY_SIZE / 2 > b_Pos.x - b_Size.x / 2 &&
            pos.x - ENEMY_SIZE / 2 < b_Pos.x + b_Size.x / 2)
                return true;
         }

        return false;
    }

    void SpawnPosGenerate()
    {
        Vector3 Size = transform.GetComponent<MeshRenderer>().bounds.size;
        float RandomPosX = Random.Range(-Size.x / 2, Size.x / 2);
        float RandomPosY = Random.Range(-Size.y / 2, Size.y / 2);
        SpownPos = transform.position + new Vector3(RandomPosX, RandomPosY, 0.0f);

        if(Collision(SpownPos))
        {
            SpawnPosGenerate();
        }
    }

    bool GoalDistanceCheck()
    {
        if(Goal.transform.position.x - transform.position.x < SpawnOffDistance)
            return false;
        return true;
    }

    bool OperatingEnemyCheck()
    {
        Enemy[] Enemys = GameObject.FindObjectsOfType<Enemy>();
        int count = 0;   

        for(int i = 0; i < Enemys.Length; i++)
        {
            if (Enemys[i].Operation == Enemys[i].stationary)
                count++;
        }

        if (count < MaxEnemy)
            return true;
        return false;
    }


    //要件定義(現時点)
    //スポーンゾーン内のランダムな場所にエネミーをスポーン
    //上空のスポーンゾーンからは特定のエネミーのみをスポーン
    //ハビタブルゾーン内の総エネミー数(=現存する非設置型のエネミー数)が一定以下(目安15)の時にスポーン処理
    //総エネミー数が常に15体になるように出現させる
    //スポーン周期は30f
}
