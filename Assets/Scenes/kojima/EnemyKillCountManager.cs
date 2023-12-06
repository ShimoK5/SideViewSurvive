using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyKillCountManager : MonoBehaviour
{
    static EnemyKillCountManager instance = null;     //マネージャーのシングルトン化
    [SerializeField] public int DestroyEnemy;       //倒したエネミー数
    [SerializeField] public int DestroyHighScoreEnemy;      //倒したハイスコアエネミー数


    static bool InitOnce = true;        //ゲーム起動時のみリセットするフラグ

    //[SerializeField] EnemyKillCountManager Instance = GameObject.Find("EnemyKillCountManager").GetComponent<EnemyKillCountManager>();       //※1他のオブジェクトにこいつ必要
    //自分でヒエラルキーから探すなら↑はいらないで、nullでオッケ

    public static EnemyKillCountManager Instance { get { return instance; } }

    void Awake()
    {
        InitOnce = true;

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
        DestroyHighScoreEnemy = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (InitOnce)
        {
            InitOnce = false;
            //初期化
            if (SceneManager.GetActiveScene().name == "ShimokawaraScene 1" || SceneManager.GetActiveScene().name == "Game")
            {
                //初期化
                DestroyEnemy = 0;
                DestroyHighScoreEnemy = 0;
            }
        }

        //ここに敵が倒れたらDestroyEnemyを１増やすようにする


        //Instance.Hoge();//※1　ほかでHogeの関数を使用するならこの書き方で問題ない 
    }

    public void DestroyCountUp() 
    {
        DestroyEnemy += 1;
        //Debug.Log(DestroyEnemy);
    }

    public void DestroyHighScoreCountUp()
    {
        DestroyHighScoreEnemy += 1;
        //Debug.Log(DestroyEnemy);
    }

    public int GetDestroyEnemy()
    {
        return DestroyEnemy;
    }

}