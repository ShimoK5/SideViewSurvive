using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    static public Player2 instance;
    PlayerState State;

    static public KeyCode jumpKey = KeyCode.Space; //ジャンプキー
    static protected float STAND_SPEED = 0.4f;
    static protected float GROUND_VEL_MULTI = 0.91f;

    public PLAYER_STATE PlayerState { get; set; } = PLAYER_STATE.AIR;

    public bool isGround = false;//地面に触れているかのフラグ
    protected Vector2 SelfVel;
    public Vector2 OtherVel;

    protected bool JumpKeyDown = false;


    protected Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void HitAround()
    {
        HitGround();
        HitUp();
        HitLeft();
        HitRight();
    }

    protected void HitGround()
    {
        isGround = false; //取り敢えず地面に接触していない事にする

        //繰り返し
        foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.3f, 0.5f, 0.5f),
            -rb.transform.up, Quaternion.identity, 100f))
        {
            float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得

            if (distance < 0.6f)
            {
                isGround = true; //地面に接触している

                if (SelfVel.y < 0.0f)
                    SelfVel.y = 0.0f;

            }
        }
    }

    protected void HitUp()
    {
        //繰り返し
        foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.3f, 0.5f, 0.5f),
            rb.transform.up, Quaternion.identity, 100f))
        {
            float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得

            if (distance < 0.6f)
            {

                if (SelfVel.y > 0.0f)
                    SelfVel.y = 0.0f;
                //Debug.Log(distance);
            }

        }
    }

    protected void HitRight()
    {
        //繰り返し
        foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.4f, 0.5f, 0.5f),
             new Vector3(1, 0, 0), Quaternion.identity, 100f))
        {
            float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得

            if (distance < 0.1f)
            {

                if (SelfVel.x > 0.0f)
                    SelfVel.x = 0.0f;

            }
            //Debug.Log(distance);
        }
    }
    protected void HitLeft()
    {
        //繰り返し
        foreach (RaycastHit hit in
            Physics.BoxCastAll(rb.transform.position, new Vector3(0.4f, 0.5f, 0.5f),
             new Vector3(-1, 0, 0), Quaternion.identity, 100f))
        {
            float distance = hit.distance; //レイの開始位置と当たったオブジェクトの距離を取得

            if (distance < 0.1f)
            {

                if (SelfVel.x < 0.0f)
                    SelfVel.x = 0.0f;

            }
            //Debug.Log(distance);
        }
    }
}
