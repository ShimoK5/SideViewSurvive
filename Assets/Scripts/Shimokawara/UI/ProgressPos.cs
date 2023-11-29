using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPos : MonoBehaviour
{
    float LocalMinX;
    float LocalMaxX;

    float WorldMinX;
    float WorldMaxX;

    Transform PlayerTF;

    RectTransform ThisRTH;

    // Start is called before the first frame update
    void Start()
    {
        LocalMinX = GameObject.Find("BarStartTemp").GetComponent<RectTransform>().localPosition.x;
        LocalMaxX = GameObject.Find("BarEndTemp").GetComponent<RectTransform>().localPosition.x;

        WorldMinX = GameObject.Find("StartObj").transform.position.x;
        WorldMaxX = GameObject.Find("GoalObj").transform.position.x;

        PlayerTF = Player.instance.transform;
        ThisRTH = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float PlayerProgressWariai
            = (PlayerTF.position.x - WorldMinX) / (WorldMaxX - WorldMinX);

        float RectLocalX = Mathf.Lerp(LocalMinX, LocalMaxX, PlayerProgressWariai);

        ThisRTH.localPosition = new Vector3(RectLocalX, ThisRTH.localPosition.y, ThisRTH.localPosition.z);
    }
}
