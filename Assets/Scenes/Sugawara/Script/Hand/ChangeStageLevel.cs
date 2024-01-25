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
}
