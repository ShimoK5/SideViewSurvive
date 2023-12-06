using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagShockWave : MonoBehaviour
{
    [Header("最大滞在距離（ワールド）")]
    [SerializeField] float MaxLifeDistance;
    [Header("最小滞在距離（ワールド）")]
    [SerializeField] float MinLifeDistance;

    float LifeDistance;   //滞在距離
    int MaxLifeCnt;          //最大滞在カウント
    int LifeCnt = 0;            //滞在カウント
    float LifeTime;       //滞在時間

    [Header("最大サイズ（ワールド）")]
    [SerializeField] float MaxSize;
    [Header("最小サイズ（ワールド）")]
    [SerializeField] float MinSize;

    float Size;   //大きさ

    Vector3 FirstPos;//初期位置

    public Vector3 Velocity; //等速直線運動のテイでの速さ

    public ParticleSystem Wave1;
    public ParticleSystem Wave2;
    public ParticleSystem Star;


    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        FirstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        LifeCnt++;

        //transform.position += Velocity;
        int Hugou = (int)Mathf.Sign(Velocity.x);
        float PosX = Easing.QuadOut(LifeCnt, MaxLifeCnt, FirstPos.x, (FirstPos.x + LifeDistance * Hugou));

        transform.position = new Vector3(PosX, transform.position.y, transform.position.z);

        if(LifeCnt >= MaxLifeCnt)
        {
            Destroy(gameObject);
        }

        //エフェクト
        // Particle Systemのmainモジュールを取得
        var WaveMainModule1 = Wave1.main;
        // 任意の新しいlifetimeを設定
        WaveMainModule1.startLifetime = LifeTime;
    }

    public void SetParam(float zeroToOne)
    {
        LifeDistance = Mathf.Lerp(MinLifeDistance, MaxLifeDistance, zeroToOne);
        Size = Mathf.Lerp(MinSize, MaxSize, zeroToOne);
        transform.localScale = new Vector3(Size, Size, Size);
        MaxLifeCnt = (int)(LifeDistance / Mathf.Abs(Velocity.x));
        LifeTime = MaxLifeCnt * (1.0f / 60);

        //エフェクト
        // Particle Systemのmainモジュールを取得
        var WaveMainModule1 = Wave1.main;
        // 任意の新しいlifetimeを設定
        WaveMainModule1.startLifetime = LifeTime;
        
        // Particle Systemのmainモジュールを取得
        var WaveMainModule2 = Wave2.main;
        // 任意の新しいlifetimeを設定
        WaveMainModule2.startLifetime = LifeTime;

        // Particle Systemのmainモジュールを取得
        var StarMainModule = Star.main;
        //particleSystemの停止
        Star.Stop();
        // 任意の新しいDurationを設定
        StarMainModule.duration = LifeTime + 0.05f;
        //particleSystemの再開
        Star.Play();
    }

    public float GetSize()
    {
        return Size;
    }
}
