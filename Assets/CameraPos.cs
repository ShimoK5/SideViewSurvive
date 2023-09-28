using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{

    [Header("Y固定")]
    [SerializeField] bool LockY;
    public static CameraPos instance;
    private GameObject targetObj;//targetオブジェクトデータ
    public float cameraPosY = 5.3f;//Y座標の加算値

    void Start()
    {
        instance = this;
        //targetオブジェクトを取得

        targetObj = GameObject.Find("Player");
        FollowObj();
    }


    public void Update()
    {
        FollowObj();
    }

    void FixedUpdate()
    {
        Update();
    }

    public void FollowObj()
    {
        if(LockY)
        {
            transform.position = new Vector3(targetObj.transform.position.x,
            transform.position.y,
            transform.position.z);
        }
        else
        {
            transform.position = new Vector3(targetObj.transform.position.x,
            targetObj.transform.position.y + cameraPosY,
            transform.position.z);
        }
    }
}


