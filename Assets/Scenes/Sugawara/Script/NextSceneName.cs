using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneName : MonoBehaviour
{
    [SerializeField] int Scene;
    private string[] SceneName = new string[2];
    private string NextScene = null;    

    public static NextSceneName Instance = null;
    void Awake()
    {
        SceneName[0] = "Game 1";
        SceneName[1] = "Game 1 Shimokawara";

        if (Scene > -1 && Scene < 2)
        {
            NextScene = SceneName[Scene];
        }
        else
        {
            NextScene = "SetScene";
        }

        Instance = this;
    }

   public string Ref_NextSceneName()
    {
        return NextScene;
    }
}
