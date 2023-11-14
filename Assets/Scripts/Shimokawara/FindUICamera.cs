using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindUICamera : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        GetComponent<Canvas>().planeDistance = 5;
    }
}
