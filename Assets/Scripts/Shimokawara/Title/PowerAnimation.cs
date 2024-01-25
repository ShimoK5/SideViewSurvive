using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerAnimation : MonoBehaviour
{
    [Header("アニメーション一枚に対するフレーム数")]
    [SerializeField] int Speed;

    [SerializeField] Sprite[] Sprites;

   
    int Cnt = 0;
    Image m_Image;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        Cnt = 0;
        m_Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Cnt++;

        //ループ
        if (Cnt >= Sprites.Length * Speed)
        {
            Cnt = 0;
        }

        int MovieCnt = Cnt / Speed;

        //書き換え
        m_Image.sprite = Sprites[MovieCnt];
    }
}
