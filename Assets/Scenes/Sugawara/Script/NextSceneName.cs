using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Data.Util.KeywordDependentCollection;

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

<<<<<<< HEAD
    public string Ref_SceneNames(int Number)
    {
        return SceneName[Number];
    }

    public void Change_SceneNumber(int SceneNumber)
=======
    public void Change_SceneNumber(SceneNumber Number)
>>>>>>> ALL_Proto_Prot
    {
        Scene = (int)Number;
        NextScene = SceneName[Scene];
    }

   public string Ref_NextSceneName()
    {
        return NextScene;
    }
}
