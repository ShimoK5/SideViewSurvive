using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_Name_Item : MonoBehaviour
{
    private Color NotUse = new Color(1.0f,0.0f,0.0f,1.0f);
    private Color Use = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    [SerializeField] private string Name = string.Empty;
    [SerializeField] private GameObject AnotherName = null;
    [SerializeField] private GameObject Before_Item = null;
    [SerializeField] private GameObject After_Item = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MaxCount()
    {
        this.GetComponent<Image>().color = NotUse;
    }

    public void Exit()
    {
        this.GetComponent<Image>().color = Use;
    }

    public void Collision()
    {
        this.GetComponent<Image>().color = Use;
        AnotherName.SetActive(true);
        After_Item.SetActive(true);
        Before_Item.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public string ItemName()
    {
        return Name;
    }
}
