using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOnlyBlock2 : Block
{
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

        Size = this.transform.GetComponent<MeshRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        OldPos = transform.position;
    }
}
