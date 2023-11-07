using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurParam : MonoBehaviour
{
    [Header("遷移変化させたいオブジェクトを入れる")]
    [SerializeField] Image m_Image;
    string ParamName = "_Blur";

    ~BlurParam()
    {
        m_Image.material.SetFloat(ParamName, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Image.material.SetFloat(ParamName, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetBlurParam(float param)
    {
        m_Image.material.SetFloat(ParamName, param);
    }
}
