using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerGaude : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float ZeroToOne = (float)(Player.instance.ScreenOutCnt) / (float)(Player.instance.MAX_SCREEN_OUT_CNT);

        image.fillAmount = ZeroToOne;
    }
}
