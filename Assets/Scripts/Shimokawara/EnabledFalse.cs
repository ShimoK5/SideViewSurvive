﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledFalse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
#if !UNITY_EDITOR
        this.GetComponent<Renderer>(). enabled = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
