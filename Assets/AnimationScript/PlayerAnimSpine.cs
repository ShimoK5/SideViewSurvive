using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class PlayerAnimSpine : MonoBehaviour
{
    public SkeletonAnimation Anim;

    int FCnt = 0;

    void Awake()
    {
        Anim = GetComponent<SkeletonAnimation>();
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

