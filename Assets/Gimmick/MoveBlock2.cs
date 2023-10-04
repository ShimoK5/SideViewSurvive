using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBlock2 : Block
{
    enum DIRECTION
    {
        Right,
        Left,
        Up,
        Down,
        RightUp,
        RightDown,
        LeftUp,
        LeftDown
    }

    [Header("ブロックの動く向き")]
    [SerializeField] DIRECTION Direction;
    [Header("ブロックの片道時間(秒数)")]
    [SerializeField] float TimeRenge;
    [Header("ブロックの片道移動量")]
    [SerializeField] float MoveRenge;
    [Header("動くのが一度きりかどうか")]
    [SerializeField] bool DoOnce;
    //[Header("↓プランナーは触らない")]
    /*public*/
    bool isMove = true;

    Vector3 MoveDirection;//動く方向
    float m_Time;//経過時間
    float PlayerHitThickness = 0.5f;//プレイヤー探索の厚み
    float NormalizeTime; //現在の経過幅を0 ~1に治した値
    float OldNormalizeTime;

    // Start is called before the first frame update
    void Start()
    {
        Size = transform.GetComponent<MeshRenderer>().GetComponent<MeshRenderer>().bounds.size;
        //FlameMove = MoveRenge / TimeRenge;
        m_Time = 0;
        VelDirection();
        NormalizeTime = 0.0f;
        OldNormalizeTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //過去座標保存
        OldPos = transform.position;
        //過去保存
        OldNormalizeTime = NormalizeTime;
        //経過時間加算
        m_Time += Time.deltaTime;
        //現在の経過幅を0~1に治す
        NormalizeTime = Mathf.Min(1.0f, m_Time / TimeRenge);
        //現在の移動量計算
        float NowMove = Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, NormalizeTime, 1, 0, MoveRenge) -
            Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, OldNormalizeTime, 1, 0, MoveRenge);

#if true
        //ブロックサイズ代入
        Vector3 ThisSize = transform.GetComponent<MeshRenderer>().bounds.size;

        //ブロックの半分の厚みで、ブロック上面からレイをスタートする
        //ボックスキャストセンター
        Vector3 CenterPos = new Vector3(transform.position.x, transform.position.y + ThisSize.y * 0.25f, transform.position.z);
        //ボックスキャストスケール
        Vector3 HalfExetents = new Vector3(ThisSize.x * 0.5f, ThisSize.y * 0.25f, ThisSize.z * 0.5f);
        //プレイヤー探索
        foreach (RaycastHit hit in
        Physics.BoxCastAll(CenterPos, HalfExetents,
        transform.up, Quaternion.identity, PlayerHitThickness))
        {
            if (hit.collider.gameObject.GetComponent<Player>())
            {
                //地上なら
                if (hit.collider.gameObject.GetComponent<Player>().GetisGround())
                {
                    //ブロックの移動に合わせて移動
                    hit.collider.gameObject.GetComponent<Player>().AddPosition((MoveDirection * NowMove));
                }
            }

            if (hit.collider.gameObject.GetComponent<Enemy>())
            {
                //地上なら
                if (hit.collider.gameObject.GetComponent<Enemy>().GetisGround())
                {
                    //ブロックの移動に合わせて移動
                    hit.collider.gameObject.GetComponent<Enemy>().AddPosition((MoveDirection * NowMove));
                }
            }
        }
#endif
        //座標移動
        transform.position += (MoveDirection * NowMove);

        //折り返し
        if (m_Time >= TimeRenge)
        {
            m_Time = 0;
            MoveDirection *= -1;
            NormalizeTime = 0;
            OldNormalizeTime = 0;
        }

        CameraPos.instance.Update();
    }

    void VelDirection()
    {
        switch (Direction)
        {
            case DIRECTION.Right:
                MoveDirection = Vector3.right;
                break;

            case DIRECTION.Left:
                MoveDirection = Vector3.left;
                break;

            case DIRECTION.Up:
                MoveDirection = Vector3.up;
                break;

            case DIRECTION.Down:
                MoveDirection = Vector3.down;
                break;

            case DIRECTION.RightUp:
                MoveDirection = new Vector3(1, 1, 0);
                break;

            case DIRECTION.RightDown:
                MoveDirection = new Vector3(1, -1, 0);
                break;

            case DIRECTION.LeftUp:
                MoveDirection = new Vector3(-1, 1, 0);
                break;

            case DIRECTION.LeftDown:
                MoveDirection = new Vector3(-1, -1, 0);
                break;
        }
    }
}
