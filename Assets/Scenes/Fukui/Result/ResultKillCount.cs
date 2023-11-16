using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultKillCount : MonoBehaviour
{

    public Text KillCountText;
    int killcount;


    // Start is called before the first frame update
    void Start()
    {
        killcount = EnemyKillCountManager.Instance.GetDestroyEnemy();

        KillCountText.text = string.Format("{0}", killcount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
