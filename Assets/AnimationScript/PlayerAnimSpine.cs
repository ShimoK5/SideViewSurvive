using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class PlayerAnimSpine : MonoBehaviour
{
    public SkeletonAnimation Anim;
    [Header("Runアニメーション再生倍率")]
    [SerializeField] float RunSpeed = 1.8f;  

    void Awake()
    {
        Anim = GetComponent<SkeletonAnimation>();
    }

    void FixedUpdate()
    {
        if (Anim.AnimationName == "normal/run")
        {
            Anim.state.TimeScale = RunSpeed;
        }
        else
        {
            Anim.state.TimeScale = 1.0f;
        }
    }

#if false
    void Start()
    {

        Anim.state.SetAnimation(0, "run", true);
    }
     
    void FixedUpdate()
    {
        FCnt++;

        if (FCnt == 120)
        {
            Anim.state.SetAnimation(0, "normal/idle", true);
        }
        if (FCnt == 190)
        {
            Anim.state.SetAnimation(0, "run", true);
        }
    }
#endif
}

