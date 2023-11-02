﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;     //マネージャーのシングルトン化
    public float m_Time;
    public int m_Score;
    int Multiply = 1;

    static bool InitOnce = true;

    void Awake()
    {

        InitOnce = true;


        if (instance == null)
        {
            instance = this;         //何もなかったらインスタンスの中身を入れるよう
            DontDestroyOnLoad(gameObject);      //他シーン行ったときに削除されないように
        }
        else
        {
            Destroy(gameObject);       //２回目の呼び出しの際にいらない分を削除
            
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(InitOnce)
        {
            InitOnce = false;
            //初期化
            if (SceneManager.GetActiveScene().name == "ShimokawaraScene 1")
            {
                //初期化
                InitGame();
            }
        }


        if (SceneManager.GetActiveScene().name == "ShimokawaraScene 1")
        {
            if(GameStateManager.instance.GameState == GAME_STATE.Game)
            {
                m_Time += Time.deltaTime;
                m_Score = Mathf.Max((1000 - (int)m_Time), 0);
                m_Score *= Multiply;
            }
        }
    }

    void InitGame()
    {
        m_Time = 0;
        m_Score = 0;
        Multiply = 0;

        //MultyPly加算
        for (int i = 0; i < RhythmManager.Instance.ActionArray.Length; i++)
        {
            if (RhythmManager.Instance.ActionArray[i] == RhythmManager.RhythmAction.None)
            {
                Multiply++;
            }
        }
    }

}