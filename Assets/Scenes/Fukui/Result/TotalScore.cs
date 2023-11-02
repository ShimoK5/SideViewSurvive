using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{

    public Text ScoreText;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = ScoreManager.instance.m_Score;

        ScoreText.text = string.Format("スコア:{0}", score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
