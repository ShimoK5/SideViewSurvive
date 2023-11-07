using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeImage : MonoBehaviour
{
    int Cnt = 0;
    public int RollEndCnt;
    Vector3 FirstRotation;

    float FirstSize;

    // Start is called before the first frame update
    void Awake()
    {
        Cnt = 0;
        FirstRotation = this.transform.localRotation.eulerAngles;
        FirstSize = this.transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Cnt++;
        if(Cnt <= RollEndCnt)
        {
            float TempCnt = Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, Cnt, RollEndCnt, 0.0f, 1.0f);

            float RoatationZ = TempCnt * 360;
            transform.localRotation = Quaternion.Euler(new Vector3((float)FirstRotation.x, (float)FirstRotation.y, (float)RoatationZ));

            float TempCnt2 = Easing.EasingTypeFloat(EASING_TYPE.SINE_INOUT, Cnt, RollEndCnt, 0.5f, 1.0f);
            float Size = TempCnt2 * FirstSize;
            transform.localScale = new Vector3((float)Size, (float)Size, (float)Size);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(FirstRotation);
        }

    }
}
