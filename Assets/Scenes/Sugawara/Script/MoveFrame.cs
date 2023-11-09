using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrame : MonoBehaviour
{
    private Vector3 NowPosition = Vector3.zero;
    private RectTransform FramePosition = null;
    private bool MoveFlag = false;
    private bool TurnFlag = false;
    [SerializeField]float MoveSpeed = 0.1f;
    [SerializeField] float TurnPower = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        FramePosition = this.gameObject.GetComponent<RectTransform>();
        MoveFlag = true;
        NowPosition = FramePosition.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MoveFlag = true;
        }

        if(MoveFlag == true)
        {
            NowPosition.x += MoveSpeed;
            
            if(NowPosition.x > -200.0f)
            {
                TurnFlag = true;
            }

            if(TurnFlag == true)
            {
                NowPosition.x -= MoveSpeed * 1.5f;
                if (NowPosition.x <= -250.0f)
                {
                    NowPosition.x = -250.0f;
                    MoveFlag = false;
                    TurnFlag = false;
                }
            }
            FramePosition.localPosition = NowPosition;
        }
    }
}
