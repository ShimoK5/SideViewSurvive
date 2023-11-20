using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewScore : MonoBehaviour
{
    TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }


    void FixedUpdate()
    {
        if(ScoreManager.instance)
        {
            Text.text = ((int)ScoreManager.instance.m_Score).ToString();
        }
    }
}
