using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDifficulty : MonoBehaviour
{

    public Text DifficultyText;
    int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        // difficulty = クラス名.getdifficulty();

        DifficultyText.text = string.Format("難易度    Lv.{0}", difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
