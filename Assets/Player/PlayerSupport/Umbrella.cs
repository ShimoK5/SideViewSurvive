using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
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
        FCnt++;

        //アングル分割
        float Angle = (90.0f / LifeCnt) * FCnt;
        //右向き
        if(Player.instance.transform.rotation.y > 0)
        {
            float AddX = 2.5f * Mathf.Cos(Angle * Mathf.Deg2Rad);
            float AddY = 2.5f * Mathf.Sin(Angle * Mathf.Deg2Rad);
            transform.position = Player.instance.transform.position;
            transform.position += new Vector3(AddX, AddY, 0);
            transform.localEulerAngles = new Vector3(0, 0, Angle);
        }
        //左向き
        else
        {
            float AddX = -2.5f * Mathf.Cos(Angle * Mathf.Deg2Rad);
            float AddY = 2.5f * Mathf.Sin(Angle * Mathf.Deg2Rad);
            transform.position = Player.instance.transform.position;
            transform.position += new Vector3(AddX, AddY, 0);
            transform.localEulerAngles = new Vector3(0, 0, -Angle);

        }

       

        if (FCnt >= LifeCnt)
        {
            Destroy(gameObject);
        }

    }

    public void InitSetPosition()
    {

        float Angle = 90.0f / LifeCnt * FCnt;

        if (Player.instance.transform.rotation.y > 0)
        {
            float AddX = 2.5f * Mathf.Cos(Angle * Mathf.Deg2Rad);
            float AddY = 2.5f * Mathf.Sin(Angle * Mathf.Deg2Rad);
            transform.position = Player.instance.transform.position;
            transform.position += new Vector3(AddX, AddY, 0);

        }
        else
        {
            float AddX = -2.5f * Mathf.Cos(Angle * Mathf.Deg2Rad);
            float AddY = 2.5f * Mathf.Sin(Angle * Mathf.Deg2Rad);
            transform.position = Player.instance.transform.position;
            transform.position += new Vector3(AddX, AddY, 0);

        }
    }
}
