using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{

    [Header("滞在時間（フレーム）")]
    [SerializeField] int LifeCnt;

    int FCnt;//フレームカウント
    public Vector3 Velocity;

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
        transform.position += Velocity;


        FCnt++;
        if (FCnt >= LifeCnt)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Block>())
        {
            Destroy (gameObject);
        }
    }
}
