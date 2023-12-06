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
    }

    public void SetParam(float zeroToOne)
    {
        LifeDistance = Mathf.Lerp(MinLifeDistance, MaxLifeDistance, zeroToOne);
        Size = Mathf.Lerp(MinSize, MaxSize, zeroToOne);
        transform.localScale = new Vector3(Size, Size, Size);
        MaxLifeCnt = (int)(LifeDistance / Mathf.Abs(Velocity.x));
        LifeTime = MaxLifeCnt * (1.0f / 60);
    }

    public float GetSize()
    {
        return Size;
    }
}
