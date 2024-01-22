using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;
using System.Text;
using System.Threading.Tasks;

public class InputName : MonoBehaviour
{
    private InputField inputFieldName; //nickname
    private RectTransform RectTransform_get;

    [SerializeField]
    private Text userName = default; //inputFieldName component

    
   void Start()
    {
        //get buttons object
        inputFieldName = GameObject.Find("InputFieldName").GetComponent<InputField>();
        RectTransform_get = GameObject.Find("InputFieldName").GetComponent<RectTransform>();
    }

    void Update()
    {
        //順位ごとに入力欄の位置調整
        switch (Result.RankCount)
        {
            case 0:
                RectTransform_get.anchoredPosition = new Vector3(-130, 124, 0);
                RectTransform_get.sizeDelta = new Vector2(209, 39);
                userName.fontSize = 20;
                break;

            case 1:
                RectTransform_get.anchoredPosition = new Vector3(-130, 66, 0);
                RectTransform_get.sizeDelta = new Vector2(209, 39);
                userName.fontSize = 20;
                break;

            case 2:
                RectTransform_get.anchoredPosition = new Vector3(-130, 13, 0);
                RectTransform_get.sizeDelta = new Vector2(209, 39);
                userName.fontSize = 20;
                break;

            case 3:
                RectTransform_get.anchoredPosition = new Vector3(-186, -39, 0);
                RectTransform_get.sizeDelta = new Vector2(121, 35);
                userName.fontSize = 15;
                break;

            case 4:
                RectTransform_get.anchoredPosition = new Vector3(-186, -92, 0);
                RectTransform_get.sizeDelta = new Vector2(121, 35);
                userName.fontSize = 15;
                break;

            case 5:
                RectTransform_get.anchoredPosition = new Vector3(-186, -141, 0);
                RectTransform_get.sizeDelta = new Vector2(121, 35);
                userName.fontSize = 15;
                break;

            case 6:
                RectTransform_get.anchoredPosition = new Vector3(156, -36, 0);
                RectTransform_get.sizeDelta = new Vector2(121, 29);
                userName.fontSize = 15;
                break;
            case 7:
                RectTransform_get.anchoredPosition = new Vector3(156, -71, 0);
                RectTransform_get.sizeDelta = new Vector2(121, 29);
                userName.fontSize = 15;
                break;

            case 8:
                RectTransform_get.anchoredPosition = new Vector3(156, -109, 0);
                RectTransform_get.sizeDelta = new Vector2(121, 29);
                userName.fontSize = 15;
                break;

            case 9:
                RectTransform_get.anchoredPosition = new Vector3(156, -146, 0);
                RectTransform_get.sizeDelta = new Vector2(121, 29);
                userName.fontSize = 15;
                break;
        }

        if (InputManager_U.instanse.GetKeyTrigger(Key.Start))
            OnClickName();
    }

    //プレイヤーネーム送信
    public void OnClickName()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;

        string name = userName.text.ToString();
        if (name.Length < 3) //3文字以下
        {
            int nameLength = name.Length;
            for (int i = 0; i < 3 - nameLength; i++)
            {
                name += "A";
            }
        }
        else if (name.Length > 8)  //8文字以上
        {
            name = name.Substring(0, 8);
        }

        DisplayName(name);       
    }


    //プレイヤーネーム表示設定
    public void DisplayName(string displayName)
    {
        // プレイヤー名の設定
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest 
            {
                DisplayName = displayName 
            },
            (result) =>
            {
                Debug.Log("Save Display Name Success!!");
                Invoke(nameof(DelaySceneManager), 1.0f);
            },
            (error) =>
            {
                Debug.LogError("Save Display Name Failed...");
                Debug.LogError(error.GenerateErrorReport());
            });   
    }

    private void DelaySceneManager()
    {
        SceneChangeManager.instance.SceneTransition("DrawRanking",0.2f, SceneChangeManager.TransitionType.Black);
    }
}