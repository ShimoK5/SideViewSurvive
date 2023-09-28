using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    [Header("バネの強さ")]
    [SerializeField] float Jump;
    GameObject myPrefab;//プレハブをGameObject型で取得
    // Start is called before the first frame update
    void Start()
    {
        myPrefab = (GameObject)Resources.Load("SpringEffect");//プレハブをGameObject型で取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().SetOuterState(PLAYER_STATE.AIR, 0, Jump, false,true,false,true);


            GameObject Obj = Instantiate(myPrefab, transform.position , Quaternion.identity);
        }
    }

}
