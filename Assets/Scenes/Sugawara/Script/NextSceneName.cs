using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneName : MonoBehaviour
{
    int MaxScene = 2;
    [SerializeField] int Scene;
    [SerializeField]private string[] SceneName = new string[2];
    private string NextScene = null;    

    public static NextSceneName Instance = null;
    void Awake()
    {

        if (Scene > -1 && Scene < MaxScene)
        {
            NextScene = SceneName[Scene];
        }
        else
        {
            NextScene = "SetScene";
        }

        Instance = this;
    }

    public string Ref_SceneNames(int Number)
    {
        return SceneName[Number];
    }

    public void Change_SceneNumber(int SceneNumber)
    {
        Scene = SceneNumber;
    }

   public string Ref_NextSceneName()
    {
        return NextScene;
    }
}
