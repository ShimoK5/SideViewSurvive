using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Name_Item : MonoBehaviour
{
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

    public void Collision()
    {
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
