using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCollision : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    [SerializeField] private Color ImageColor;
    [SerializeField] private float ChangeSpeedAlfa = 0.01f;
    [SerializeField] public bool ChangeAlfa = false;

    // Start is called before the first frame update
    void Awake()
    {
        Parent = this.transform.parent.gameObject;
        ImageColor = Color.white;
        ImageColor.a = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(ChangeAlfa == false)
        {
            ImageColor.a -= ChangeSpeedAlfa;
            if(ImageColor.a <= 0.8f)
            {
                ImageColor.a = 0.8f;
            }
        }
        else
        {
            ImageColor.a += ChangeSpeedAlfa;
            if (ImageColor.a >= 1.0f)
            {
                ImageColor.a = 1.0f;
            }
        }
        Parent.gameObject.GetComponent<Image>().color = ImageColor;
    }

    public void TouchImage()
    {
        ChangeAlfa = true;
    }

    public void DontTouchImage()
    {
        ChangeAlfa = false;
    }
}
