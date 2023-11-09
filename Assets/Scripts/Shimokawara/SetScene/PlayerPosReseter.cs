using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosReseter : MonoBehaviour
{
    int NowCnt = 0;
    int BeatTempo = 30;

    bool OverOnce = false;

    Vector3 DefaultPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        DefaultPos = Player.instance.transform.position;
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
                Player.instance.transform.position = DefaultPos;
            }            
        }

        else if(NowCnt < BeatTempo - 2)
        {
            OverOnce = false;
        }
    }
}
