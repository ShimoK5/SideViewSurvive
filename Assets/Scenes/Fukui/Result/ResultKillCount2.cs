using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultKillCount2 : MonoBehaviour
{
    TextMeshProUGUI KillCountText;
    int killcount;

    // Start is called before the first frame update
    void Start()
    {
        KillCountText = GetComponent<TextMeshProUGUI>();

        killcount = EnemyKillCountManager.Instance.GetDestroyEnemy() + EnemyKillCountManager.Instance.DestroyHighScoreEnemy;
        KillCountText.text = (killcount).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
