using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTime : MonoBehaviour
{

    public Text TimeText;
    int time;

    // Start is called before the first frame update
    void Start()
    {
        // time = クラス名.gettime();

        TimeText.text = string.Format("タイム:{0}", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
