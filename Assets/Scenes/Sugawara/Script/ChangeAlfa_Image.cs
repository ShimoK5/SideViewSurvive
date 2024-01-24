using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAlfa_Image : MonoBehaviour
{

    Color TextColor = Color.white;
    float ChangeSpeed = 0.04f;
    bool Change = false;

    // Start is called before the first frame update
    void Awake()
    {
        TextColor.a = 0.0f;
        this.gameObject.GetComponent<Image>().color = TextColor;
        Change = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Change == false)
        {
            TextColor.a += ChangeSpeed;
            if (TextColor.a > 1.0f)
            {
                TextColor = Color.white;
            }
            this.GetComponent<Image>().color = TextColor;
        }
        else
        {

        }
    }
}
