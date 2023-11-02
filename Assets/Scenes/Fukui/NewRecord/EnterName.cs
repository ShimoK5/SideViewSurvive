using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterName : MonoBehaviour
{

    //オブジェクトと結びつける
    public InputField inputField;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        //Componentを扱えるようにする
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        text.text = inputField.text;

    }
}
