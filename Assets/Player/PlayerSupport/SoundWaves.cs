using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaves : MonoBehaviour
{

    [Header("滞在時間")]
    [SerializeField] int LifeCnt;

    int FCnt;//フレームカウント
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
        transform.position = Player.instance.transform.position;
        FCnt++;
        if (FCnt >= LifeCnt)
        {
            Destroy(gameObject);
        }

    }
}
