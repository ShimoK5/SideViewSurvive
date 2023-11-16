using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyDeadData : MonoBehaviour
{

    [Header("エネミーやられ吹っ飛び勢い")]
    [SerializeField] public float DeadVectorMulti;

    [Header("エネミー消滅までの時間")]
    [SerializeField] public int DeadFlame;

    public static TempEnemyDeadData instance;

    void Awake()
    {
        instance = this;
    }

}
