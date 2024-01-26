using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStageLevel : MonoBehaviour
{
    public static ChangeStageLevel instance;
    [SerializeField] Sprite[] LevelSprite = new Sprite[2];

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void EasyLevel()
    {
        this.gameObject.GetComponent<Image>().sprite = LevelSprite[0];
    }

    public void NormalLevel()
    {
        this.gameObject.GetComponent<Image>().sprite = LevelSprite[1];
    }

    public void SetStageLevel()
    {
        string Stage = InputRhythm.instance.GetSceneName();
        switch (Stage)
        {
            case "Game":
                this.gameObject.GetComponent<Image>().sprite = LevelSprite[0];
                break;

            case "Game Hard":
                this.gameObject.GetComponent<Image>().sprite = LevelSprite[1];
                break;

            default:
                this.gameObject.GetComponent<Image>().sprite = LevelSprite[0];
                break;
        }
    }
}
