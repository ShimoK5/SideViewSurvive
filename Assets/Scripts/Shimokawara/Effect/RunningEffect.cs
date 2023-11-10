using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEffect : MonoBehaviour
{
    bool isGround;
    bool OldisGround;
    // Start is called before the first frame update
    void Start()
    {
        isGround = OldisGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Player.instance.GetisGround();

        if(isGround != OldisGround)
        {
            if(isGround)
            {
                GetComponent<ParticleSystem>().Play(true);
            }
            else
            {
                GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }

#if false
        //ParticleSystem.EmissionModule emission = GetComponent<ParticleSystem>().emission;

        if (Player.instance.GetisGround())
        {
            //GetComponent<ParticleSystem>().Play(true);
            //emission.enabled = true;
            Debug.Log("有効");
        }
        else
        {
            GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
            //emission.enabled = false;
            Debug.Log("無向");
        }

#endif
        OldisGround = isGround;
    }
}
