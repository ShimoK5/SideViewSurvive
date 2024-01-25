using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneName : MonoBehaviour
{
    public static NextSceneName instance = null;
    public enum SceneNumber
    {
        Button_L_Scene = 0,
        Button_R_Scene = 1,
        TestScene = 2,
    }

    int MaxScene = 2;
    [SerializeField] int Scene;
    [SerializeField]private string[] SceneName = new string[3];
    private string NextScene = null;    

   
    void Awake()
    {
        instance = this;

        if (Scene > -1 && Scene < MaxScene)
        {
            NextScene = SceneName[Scene];
        }
        else
        {
            NextScene = "SetScene";
        }
    }

    public void Change_SceneNumber(SceneNumber Number)
    {
        Scene = (int)Number;
        NextScene = SceneName[Scene];
    }

   public string Ref_NextSceneName()
    {
        return NextScene;
    }
}
