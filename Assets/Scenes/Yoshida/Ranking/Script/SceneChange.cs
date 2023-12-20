using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChange : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (InputManager_U.instanse.GetKeyTrigger(Key.Start) || InputManager_U.instanse.GetKeyTrigger(Key.B))
            TitleChange();
    }

    //ボタン処理
    public void TitleChange()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;
        Debug.Log("タイトルに移動");
        SceneChangeManager.instance.SceneTransition("UI_title");
    }
}
