using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAlfa : MonoBehaviour
{
    Color TextColor = Color.black;
    float ChangeSpeed = 0.04f;
    bool Change = false;

    // Start is called before the first frame update
    void Start()
    {
        TextColor.a = 0.0f;
        this.gameObject.GetComponent<Text>().color = TextColor;
        Change = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Change == false)
        {
            TextColor.a += ChangeSpeed;
            if (TextColor.a > 1.0f)
            {
                TextColor = Color.black;
            }
            this.GetComponent<Text>().color = TextColor;
        }
        else
        {

        }
    }
}
