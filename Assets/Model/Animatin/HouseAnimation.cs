﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAnimation : MonoBehaviour
{
    Animator m_Animator;
    bool OnceAnim = true;
    int Cnt = 0;

    void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameStateManager.instance.GameState == GAME_STATE.StartPlayerMotion)
        {
            if(OnceAnim)
            {
                PlayOpenAnim();
                OnceAnim = false;
            }

            Cnt++;

            if(Cnt >= 12 )
            {
                GameStateManager.instance.GameState = GAME_STATE.Game;
            }

        }
    }

    void PlayOpenAnim()
    {
        m_Animator.Play("Main_house|Main_house|Take 001|BaseLayer");
    }
}