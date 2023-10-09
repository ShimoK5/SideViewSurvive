using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.instance.transform.position;
    }

    void FixedUpdate()
    {
        Update();
    }
}
