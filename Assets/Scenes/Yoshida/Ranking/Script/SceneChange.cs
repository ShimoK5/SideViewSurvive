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
        {
            NewSoundManager.instance.PlaySE("決定音");
            TitleChange();
        }
    }

    //ボタン処理
    public void TitleChange()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;
       
        SceneChangeManager.instance.SceneTransition("UI_title", 0.2f, SceneChangeManager.TransitionType.Black);
    }
}
