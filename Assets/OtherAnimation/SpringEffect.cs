using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEffect : MonoBehaviour
{
    public SpringEffectSoro[] SoroArray = new SpringEffectSoro[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //初期値false
        bool DestroyTrigger = false;
        for(int i = 0; i < SoroArray.Length;i++)
        {
            if(!SoroArray[i])
            {
                DestroyTrigger = true;
            }
        }

        if(DestroyTrigger)
        {
            Destroy(this.gameObject);
        }
    }
}
