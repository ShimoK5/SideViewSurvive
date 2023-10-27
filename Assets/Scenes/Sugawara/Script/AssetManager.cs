using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public enum ActionNumber
    {
        Umbrella = 1,     //体操着
        Recorder,       //リコーダー
        Eraser,         //消しゴム
        Sacrifice,      //身代わり
        AirCannon,      //空気砲
        Bag,            //ランドセル
        Ruler,          //定規
        Whistle,        //笛
        None            //なし
    }

    public enum AssetTypeNumber
    {
        BGM,
        SE
    }

    public static AssetManager Instance;
    public Sprite[] SpriteAsset = default;
    public AudioClip[] SEAsset = default;
    public AudioClip[] BGMAsset = default;
    private AudioSource audioSource = null;
    private bool PlaySESound = false;
    private bool PlayBGMSound = false;
    
    public class ReserveAsset
    {
        public AssetTypeNumber AssetType;
        public ActionNumber AssetNumber;
    }
    [SerializeField] List<ReserveAsset>ReserveAssets = new List<ReserveAsset>();


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        PlaySESound = false;
        PlayBGMSound = false;
        //AssetNumber = default;

        Addreserve(AssetTypeNumber.BGM, ActionNumber.AirCannon);
        Addreserve(AssetTypeNumber.SE, ActionNumber.Bag);
        Addreserve(AssetTypeNumber.SE, ActionNumber.Ruler);
        Addreserve(AssetTypeNumber.BGM, ActionNumber.Sacrifice);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlaySESound == true &&(Input.GetKey(KeyCode.A)))
        {
            PlaySESound = false;

            Debug.Log(ReserveAssets[0].AssetType);
            Debug.Log(ReserveAssets[0].AssetNumber);

            ReserveAssets.Remove(ReserveAssets[0]);

            Debug.Log(ReserveAssets[0].AssetType);
            Debug.Log(ReserveAssets[0].AssetNumber);
            
            ReserveAssets.Remove(ReserveAssets[0]);

            Debug.Log(ReserveAssets[0].AssetType);
            Debug.Log(ReserveAssets[0].AssetNumber);

            ReserveAssets.Remove(ReserveAssets[0]);
            if (ReserveAssets.Count > 0)
            {
                Debug.Log(ReserveAssets[0].AssetType);
                Debug.Log(ReserveAssets[0].AssetNumber);
            }else
            {
                Debug.Log("ReserveAssets[3]が存在しないよ");
            }
           
        };

        //if((Input.GetKey(KeyCode.Q)))
        //{
        //    PlaySESound = true;
        //}
        //else
        //{
        //    PlaySESound = false;
        //}
    }

    public void PlaySE(int Number)
    {
        PlaySESound = true;
    }

    void Addreserve(AssetTypeNumber type, ActionNumber number)
    {
        ReserveAsset Temporary = new ReserveAsset();
        Temporary.AssetType = type;
        Temporary.AssetNumber = number;
        ReserveAssets.Add(Temporary);

        if(PlaySESound == false)
        {
            PlaySESound = true;
        }
    }

}

