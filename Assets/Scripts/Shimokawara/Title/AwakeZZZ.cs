using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeZZZ : MonoBehaviour
{
    int FCnt = 0;
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        FCnt = 0;
        m_Animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        FCnt++;

        if(FCnt >= 68)
        {
            //アニメーション再生
            m_Animator.Play("Animation_zzz");
            this.enabled = false;
        }
    }

}
