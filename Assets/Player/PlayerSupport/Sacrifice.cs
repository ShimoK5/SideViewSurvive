using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice: PawnIF
{
    static float MultiplyNum = 1.0f / 60;
    static protected float GLAVITY = 0.68f * MultiplyNum;
    static protected float GROUND_VEL_MULTI = 0.81f;    //地上減速（摩擦）
    static protected float AIR_VEL_MULTI = 0.998f;      //空中減速（空気抵抗）

    bool isGround = false;

    [Header("存在する時間（フレーム）")]
    [SerializeField] int LifeCnt;
    int FCnt = 0;
    Vector2 Vel = Vector2.zero;

    // Start is called before the first frame update
    void Awake()
    {
        FCnt = 0;

        tf = GetComponent<Transform>();
        OldPos = tf.transform.position;
        //tf.transform.localEulerAngles = new Vector3(0, 90, 0);
        Size = tf.transform.GetComponent<MeshRenderer>().GetComponent<MeshRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
#if false
        // Sacrifice クラス型の配列に、シーン上すべてのSacrificeをぶち込む
        Sacrifice[] SacrificeArray = GameObject.FindObjectsOfType<Sacrifice>();

        //身代わり一個もない（プレイヤーしかいない）
        if(SacrificeArray.Length == 0)//    ←「配列名.Length」で要素数取得できるよん
        {
            //プレイヤーに追従するように書いて～
        }
        //身代わり一個以上ある
        else
        {
            // 距離保存変数用意するよね多分
            // float TempDistance;

            // 一番近い奴の要素数保存用の変数も用意しそう
            // int NeerIndex;

            // for回す
            for (int i = 0; i < SacrificeArray.Length; i++)
            {
                //一番近いやつ見つけそう
            }

            //一番近い奴に追従するように書く？
        }
#endif
    }

    void FixedUpdate()
    {
        OldPos = tf.transform.position;
        Vel.y -= GLAVITY;
        if(isGround)
        {
            Vel.x *= GROUND_VEL_MULTI;
        }
        else
        {
            Vel.x *= AIR_VEL_MULTI;
        }
        

        tf.transform.position += new Vector3(Vel.x, Vel.y, 0);
        DeadCheck();
    }

    void DeadCheck()
    {
        FCnt++;
        if (FCnt >= LifeCnt)
        {
            //GameObject myPrefab;//プレハブをGameObject型で取得
            //myPrefab = (GameObject)Resources.Load("SpringEffect");//プレハブをGameObject型で取得
            //GameObject Obj = Instantiate(myPrefab, tf.position, Quaternion.identity);

            GameObject Effect;//プレハブをGameObject型で取得
            Effect = (GameObject)Resources.Load("Prefabs/vfx_SacrificeDeath");//プレハブをGameObject型で取得
            GameObject Obj = Instantiate(Effect, tf.position, Quaternion.identity);


            Destroy(tf.gameObject);
        }
    }

    public void SetVel(Vector2 vel)
    {
        Vel = vel;
    }





    public override void HitUnder(Block block)
    {
        isGround = true;
        StandBlock = block;
        float YPos = block.transform.position.y + (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        
        if (Vel.y > -0.1f)
        {
            Vel.y = 0;
        }
        else
        {
            Vel.y *= -0.5f;
        }
       

    }
    public override void HitTop(Block block)
    {
        float YPos = block.transform.position.y - (block.Size.y + Size.y) / 2;
        tf.transform.position = new Vector3(tf.transform.position.x, YPos, tf.transform.position.z);
        Vel.y = 0.0f;
    }
    public override void HitRight(Block block)
    {
        float XPos = block.transform.position.x - (block.Size.x + Size.x) / 2;
        tf.transform.position = new Vector3(XPos, tf.transform.position.y, tf.transform.position.z);
        Vel.x *= -0.5f;
    }
    public override void HitLeft(Block block)
    {
        float XPos = block.transform.position.x + (block.Size.x + Size.x) / 2;
        tf.transform.position = new Vector3(XPos, tf.transform.position.y, tf.transform.position.z);
        Vel.x *= -0.5f;
    }
    public override void NonHitUnder()
    {
        isGround = false;
        StandBlock = null;
    }

    public void AddPosition(Vector3 AddVolume)
    {
         transform.position += AddVolume;
    }


    
}
