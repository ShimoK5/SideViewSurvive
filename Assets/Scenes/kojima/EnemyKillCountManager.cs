using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillCountManager : MonoBehaviour
{
    static EnemyKillCountManager instance = null;     //マネージャーのシングルトン化
    [SerializeField] public int DestroyEnemy;       //倒したエネミー数

    //[SerializeField] EnemyKillCountManager Instance = GameObject.Find("EnemyKillCountManager").GetComponent<EnemyKillCountManager>();       //※1他のオブジェクトにこいつ必要
    //自分でヒエラルキーから探すなら↑はいらないで、nullでオッケ
    
    public static EnemyKillCountManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;        
        }
        else
        {
            Destroy(gameObject); 
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); 
        DestroyEnemy = 0;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ここに敵が倒れたらDestroyEnemyを１増やすようにする
        

        //Instance.Hoge();//※1　ほかでHogeの関数を使用するならこの書き方で問題ない 
    }

    public void DestroyCountUp()          //別のスクリプトで関数呼び出し愛時にはこんな感じで作成　※1
    {
        DestroyEnemy += 1;
        Debug.Log(DestroyEnemy);
    }
}