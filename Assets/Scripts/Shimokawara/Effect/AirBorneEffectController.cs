using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBorneEffectController : MonoBehaviour
{
    public GameObject AirBoneEffect; 

    // Start is called before the first frame update
    void Start()
    {
        AirBoneEffect.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.instance.GetM_Player().NowAirBorne)
        {
            AirBoneEffect.SetActive(true);
       

            //emission.enabled = true;
            //this.gameObject.SetActive(true);
        }
        else
        {
            AirBoneEffect.SetActive(false);
            //emission.enabled = false;
            //this.gameObject.SetActive(false);
        }
    }
}
