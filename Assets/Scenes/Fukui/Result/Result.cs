﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") /*|| Input.GetButtonDown("Action1")*/) //スペースキー、Aボタン
        {
            SceneManager.LoadScene("NewRecord");//some_senseiシーンをロードする
        }
    }
}
