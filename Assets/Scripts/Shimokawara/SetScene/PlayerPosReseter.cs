using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosReseter : MonoBehaviour
{
    int NowCnt = 0;
    int BeatTempo = 30;

    bool OverOnce = false;

    ParticleSystem ps;

    Vector3 DefaultPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        DefaultPos = Player.instance.transform.position;
        ps = GameObject.Find("vfx_PlayerWalking").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        NowCnt = RhythmManager.Instance.FCnt;
        BeatTempo = RhythmManager.Instance.BeatTempo;

        if (NowCnt >= BeatTempo - 2 && !OverOnce)
        {
            OverOnce = true;
            if(Player.instance.transform.position.x > 100f)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                Player.instance.transform.position = DefaultPos;
                ps.Play(true);
            }            
        }

        else if(NowCnt < BeatTempo - 2)
        {
            OverOnce = false;
        }
    }
}
