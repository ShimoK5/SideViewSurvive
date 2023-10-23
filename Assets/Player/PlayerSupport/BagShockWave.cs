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

    [Header("最大サイズ（ワールド）")]
    [SerializeField] float MaxSize;
    [Header("最小サイズ（ワールド）")]
    [SerializeField] float MinSize;

    float Size;   //大きさ

    Vector3 FirstPos;//初期位置

    public Vector3 Velocity;

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
        transform.position += Velocity;


        if((transform.position - FirstPos).magnitude >= LifeDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetParam(float zeroToOne)
    {
        LifeDistance = Mathf.Lerp(MinLifeDistance, MaxLifeDistance, zeroToOne);
        Size = Mathf.Lerp(MinSize, MaxSize, zeroToOne);
        transform.localScale = new Vector3(Size, Size, Size);
    }

    public float GetSize()
    {
        return Size;
    }
}
