using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class MoveBlock : MonoBehaviour
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
    [Header("ブロックの片道時間(フレーム)")]
    [SerializeField] float TimeRenge;
    [Header("ブロックの片道移動量")]
    [SerializeField] float MoveRenge;
    [Header("動くのが一度きりかどうか")]
    [SerializeField] bool DoOnce;
    //[Header("↓プランナーは触らない")]
    /*public*/ bool isMove = true;

    Vector3 MoveDirection;//動く方向
    float FlameMove;//1Fの移動量
    int FCnt;
    float PlayerHitThickness = 0.1f;//プレイヤー探索の厚み

    // Start is called before the first frame update
    void Start()
    {
        FlameMove = MoveRenge / TimeRenge;
        FCnt = 0;
        VelDirection();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ////ブロックサイズ代入
        //Vector3 ThisSize = transform.GetComponent<MeshRenderer>().bounds.size;

        ////ブロックの半分の厚みで、ブロック上面からレイをスタートする
        ////ボックスキャストセンター
        //Vector3 CenterPos = new Vector3(transform.position.x, transform.position.y + ThisSize.y * 0.25f, transform.position.z);
        ////ボックスキャストスケール
        //Vector3 HalfExetents = new Vector3(ThisSize.x * 0.5f, ThisSize.y * 0.25f, ThisSize.z * 0.5f);
        ////プレイヤー探索
        
        //foreach (RaycastHit hit in
        //Physics.BoxCastAll(CenterPos, HalfExetents,
        //transform.up, Quaternion.identity, PlayerHitThickness))
        //{
        //    if(hit.collider.gameObject.GetComponent<Player>())
        //    {
        //        //ブロックの移動に合わせて移動
        //        hit.collider.gameObject.GetComponent<Player>().AddPosition((MoveDirection * FlameMove));
        //    }
        //}


        //座標移動
        transform.position += (MoveDirection * FlameMove);

        //折り返し
        FCnt++;
        if (FCnt >= TimeRenge)
        {
            FCnt = 0;
            MoveDirection *= -1;
        }
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
