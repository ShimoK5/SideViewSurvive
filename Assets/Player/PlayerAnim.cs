using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnim : MonoBehaviour
{
    static public PlayerAnim instans;
    public Animator Anim;

    void Awake()
    {
        instans = this;
        Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
