using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOnlyBlock : MonoBehaviour
{
    Rigidbody m_Player;
    Vector3 m_PlayerSize;
    Vector3 m_SelfSize;

    Vector3 m_PlayerPos;
    Vector3 m_OldPlayerPos;

    float m_Thickness = 0.2f; //厚み

    // Start is called before the first frame update
    void Start()
    {
        //幅を縮める前に座標を移動する
        this.transform.position = new Vector3(transform.position.x,
            transform.position.y + (this.transform.GetComponent<MeshRenderer>().bounds.size.y - m_Thickness) * 0.5f,
            transform.position.z);
        //厚みを縮める
        this.transform.localScale = new Vector3(transform.localScale.x, m_Thickness, transform.localScale.z);

        m_Player = Player.instance.GetComponent<Rigidbody>();
        m_PlayerSize = m_Player.transform.GetComponent<MeshRenderer>().bounds.size;
        m_SelfSize = this.transform.GetComponent<MeshRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        m_PlayerPos = m_Player.transform.position;

        //プレイヤーの一番下座標の少し上(カプセルの丸みより上)
        float PlayerUnder = m_PlayerPos.y - m_PlayerSize.y / 2 + m_PlayerSize.y * 0.125f;
        //プレイヤーの一番下座標の少し上(カプセルの丸みより上)(1フレ前)
        float OldPlayerUnder = m_OldPlayerPos.y - m_PlayerSize.y / 2 + m_PlayerSize.y * 0.125f;
        //ブロックの一番上座標
        float BlockTop = transform.position.y + m_SelfSize.y / 2;
        //ブロックの真ん中の座標
        //float ThisCenter = transform.position.y ;

        //1フレ前か今のどちらかで上にいたならブロック実体
        if (PlayerUnder > BlockTop|| OldPlayerUnder > BlockTop)
        {
            //あたり判定がある処理を書く
            this.GetComponent<Collider>().isTrigger = false;
            this.tag = "Platform_Default";

        }
        else
        {
            //あたり判定がない処理を書く
            this.GetComponent<Collider>().isTrigger = true;
            this.tag = "Untagged";

        }
    }
}
