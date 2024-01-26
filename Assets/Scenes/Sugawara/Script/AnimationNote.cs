using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNote : MonoBehaviour
{
    private Animator m_Animator;

    void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
    }

   public void HightLighted()
    {
        m_Animator.SetTrigger("HightLighted");
    }

    public void Normal()
    {
        m_Animator.SetTrigger("Normal");
    }
}
