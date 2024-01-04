using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGauge : MonoBehaviour
{
    private Image image;

    private float param = 0;

    [SerializeField]
    private float speed = 0.05f;

    [SerializeField]
    private string nextSceneName;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.fillAmount = param;
    }

    void FixedUpdate()
    {
        if (InputManager_FU.instanse.GetKey(Key.B))
        {
            if ((param += speed) >= 1)
            {
                SceneChangeManager.instance.SceneTransition(nextSceneName);
                //SE
                NewSoundManager.instance.PlaySE("決定音");
                this.enabled = false;
                //param = 0;
            }
        }
        else
        {
            if (param > 0)
            {
                param = 0;
            }
        }
        image.fillAmount = param;
    }

    public void SetSceneName(string Name)
    {
        nextSceneName = Name;
    }
}
