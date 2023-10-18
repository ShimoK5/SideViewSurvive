using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveHand : MonoBehaviour
{
    [SerializeField] private Vector3 HandPosition = new Vector3(50.0f, -82.0f, 0.0f);       //ハンドの初期位置
    [SerializeField] private float MoveSpeed = 0.1f;                                        //移動スピード
    [SerializeField] private RectTransform Icon_Transform = null;                           //アイコンの位置、話したときに戻す用
    [SerializeField] private bool DragJudge = false;
    [SerializeField] private bool DragAndDrop = false;                                 //アイコンを掴めるかどうかの確認
    [SerializeField] private GameObject Catch_Icon = null;                                  //アイコン保管用（多分消せる）
    [SerializeField] private Vector3 Catch_IconPosition = Vector3.zero;                     //移動する際に使用する位置座標（後で変数にして削除予定）
    public static MoveHand instance;
    [SerializeField] public GameObject[] NoteBox = new GameObject[8];

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        HandPosition = new Vector3(50.0f, -82.0f, 0.0f);
        Icon_Transform = this.GetComponent<RectTransform>();
        Icon_Transform.TransformPoint(HandPosition);
        DragAndDrop = false;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnCollisionEnter2D: " + collision.gameObject.name);      //確認用　あとで削除予定
        if (DragAndDrop == false && collision.gameObject.name == "Icon(Clone)")
        {
            DragJudge = true;
            Catch_Icon = collision.gameObject;
            Catch_IconPosition = Catch_Icon.GetComponent<RectTransform>().localPosition;
        }
        else if (DragAndDrop == false && collision.gameObject.name == "NextStage")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            DragJudge = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("OnCollisionExit2D: " + collision.gameObject.name);
        //SelectImage = null;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
            if (DragJudge == true)
            {
                if (Input.GetKeyDown("joystick button 1"))
                {
                    DragAndDrop = true;
                }
            }
        }
        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            if (DragJudge == true)
            {
                if (Input.GetKeyDown("joystick button 1"))
                {
                    DragAndDrop = true;


                }
            }
            MoveChange();
            //HandPosition = new Vector3(HandPosition.x + MoveSpeed * Input.GetAxis("Vertical"), HandPosition.y + MoveSpeed * Input.GetAxis("Horizontal"), 0.0f);
            Icon_Transform.localPosition = HandPosition;
            if (DragAndDrop == true)
            {
                Catch_Icon.GetComponent<RectTransform>().localPosition = Catch_IconPosition;
            }           
        }        
    }

    void MoveChange()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            HandPosition.x = HandPosition.x + MoveSpeed;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.x += MoveSpeed;
            }

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            HandPosition.x = HandPosition.x + MoveSpeed * -1.0f;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.x += MoveSpeed * -1.0f;
            }
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            HandPosition.y = HandPosition.y + MoveSpeed;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.y += MoveSpeed;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            HandPosition.y = HandPosition.y + MoveSpeed * -1.0f;
            if (DragAndDrop == true)
            {
                Catch_IconPosition.y += MoveSpeed * -1.0f;
            }
        }
    }

    void DoCarryIcon()
    {

    }
}