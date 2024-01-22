using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScore2 : MonoBehaviour
{
    TextMeshProUGUI ScoreText;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GetComponent<TextMeshProUGUI>();

        score = ScoreManager.instance.m_Score;
        ScoreText.text = (score).ToString();
    }


    void FixedUpdate()
    {

    }
}
