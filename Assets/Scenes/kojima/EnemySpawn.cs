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
    private int EnemyCount = 0;
    private int ZigZagCount = 0;
    private int TrackingCount = 0;


    [Header("エネミーの存在数の最低保証")]
    [SerializeField] int TrackingFew = 3;
    [SerializeField] int ZigZagFew = 3;

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
        if(GameStateManager.instance.GameState == GAME_STATE.Game)
            FlameCount++;

        if (FlameCount > 30)
            FlameCount = 0;


        if (FlameCount == 30 && GoalDistanceCheck() && OperatingEnemyCheck())
        {
            float TrackingSpawnRate = 0.5f;
            //if ((TrackingCount < TrackingFew && ZigZagCount < ZigZagFew) || (TrackingCount == ZigZagCount))
            //    TrackingSpawnRate = 0.5f;
            //else if(TrackingCount < ZigZagCount)
            //    TrackingSpawnRate = (TrackingCount - TrackingFew) / ZigZagCount;
            ////else if(TrackingCount > ZigZagCount)
            ////    TrackingSpawnRate = (ZigZagCount - ZigZagFew) / (TrackingCount);

            //現存数のうち追尾が少ない→追尾の出現確率を上げる＝ジグザグの出現率を下げる
            //現存数のうちジグザグが少ない（略
            //現存数のうち追尾が極めて少ない→追尾を100%出現にする



            int choice;

            choice = (RandomBool(TrackingSpawnRate)) ? 0 : 1;
            //choice = Random.Range(0, 1);
            GameObject Enemy;

            switch (choice)
            {
                case 0:
                    Enemy = (GameObject)Resources.Load("EnemyTracking");
                    break;
                case 1:
                    Enemy = (GameObject)Resources.Load("EnemyZigZag");
                    break;
                    
                default:
                    Enemy = null;
                    break;
            }
            SpawnPosGenerate();

            Instantiate(Enemy, SpownPos, Quaternion.Euler(0, 0, 0));
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

    //出現場所の設定
    void SpawnPosGenerate()
    {
        Vector3 Size = transform.GetComponent<MeshRenderer>().bounds.size;
        float RandomPosX = Random.Range(-Size.x / 2, Size.x / 2);
        float RandomPosY = Random.Range(-Size.y / 2, Size.y / 2);
        SpownPos = transform.position + new Vector3(RandomPosX, RandomPosY, -0.5f);

        if(Collision(SpownPos))
        {
            SpawnPosGenerate();
        }
    }

    //ゴールとの距離確認
    bool GoalDistanceCheck()
    {
        if(Goal.transform.position.x - transform.position.x < SpawnOffDistance)
            return false;
        return true;
    }

    //稼働済みエネミー数の確認(ついでにそれぞれのエネミー種の残存数確認)
    bool OperatingEnemyCheck()
    {
        Enemy[] Enemys = GameObject.FindObjectsOfType<Enemy>();
        int count = 0;

        for(int i = 0; i < Enemys.Length; i++)
        {
            if (Enemys[i].Operation == Enemys[i].stationary)
            {
                count++;
                if (Enemys[i].GetEnemyState() == ENEMY_STATE.ZIGZAG)
                {
                    ZigZagCount++;
                }
                else if (Enemys[i].GetEnemyState() == ENEMY_STATE.TRACKING)
                {
                    TrackingCount++;
                }
            }
        }

        EnemyCount = count;

        if (count < MaxEnemy)
            return true;
        return false;
    }

    //bool型乱数　引数：成功確率(0.0f～1.0f)
    bool RandomBool(float successRate)
    {
        return Random.Range(0.0f, 1.0f) <= successRate;
    }

    //要件定義(現時点)
    //スポーンゾーン内のランダムな場所にエネミーをスポーン
    //上空のスポーンゾーンからは特定のエネミーのみをスポーン
    //ハビタブルゾーン内の総エネミー数(=現存する非設置型のエネミー数)が一定以下(目安15)の時にスポーン処理
    //総エネミー数が常に15体になるように出現させる
    //スポーン周期は30f
}
