using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultDifficulty2 : MonoBehaviour
{
    TextMeshProUGUI DifficultyText;
    int Difficulty;

    // Start is called before the first frame update
    void Start()
    {
        DifficultyText = GetComponent<TextMeshProUGUI>();

        Difficulty = ActionLevel.instance.GetLevel();
        DifficultyText.text = (Difficulty).ToString();
    }


    void FixedUpdate()
    {

    }
}
