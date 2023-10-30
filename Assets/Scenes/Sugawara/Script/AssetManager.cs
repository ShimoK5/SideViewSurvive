using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public enum  ActionNumber               //アクション名
    {
        Umbrella,     //体操着
        Recorder,       //リコーダー
        Eraser,         //消しゴム
        Sacrifice,      //身代わり
        AirCannon,      //空気砲
        Bag,            //ランドセル
        Ruler,          //定規
        Whistle,        //笛
        None            //なし
    }

    public enum AssetTypeNumber             //アセット一覧
    {
        BGM,
        SE
    }

    public static AssetManager Instance;        //シングルトンするためのインスタンス
    [SerializeField] private Sprite[] SpriteAsset = default;      //格納するスプライトの配列
    [SerializeField] private AudioClip[] SEAsset = default;       //格納するSEの配列
    [SerializeField] private AudioClip[] BGMAsset = default;      //格納するBGMの配列
    [SerializeField] private float AudioVolume = 0.0f;
    private AudioSource audioSource = null;     //音鳴らすための物
    
    public class ReserveAsset               //音鳴らすために予約をするクラス
    {
        public AssetTypeNumber AssetType;
        public ActionNumber AssetNumber;
    }
    [SerializeField] List<ReserveAsset>ReserveAssets = new List<ReserveAsset>();        //リスト化


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();         //自身の持っているオーディオソースを持ってくる
        ReserveAssets.Clear();                                  //リストの中身を初期化

        if(AudioVolume <=0.0f)
        {
            AudioVolume = 0.03f;
        }
        //中身問題ないかの確認用
        //AddMusicReserve(AssetTypeNumber.BGM, ActionNumber.AirCannon);
        //AddMusicReserve(AssetTypeNumber.SE, ActionNumber.Bag);
        //AddMusicReserve(AssetTypeNumber.SE, ActionNumber.Ruler);
        //AddMusicReserve(AssetTypeNumber.BGM, ActionNumber.Sacrifice);
    }

    // Update is called once per frame
    void Update()
    {
        if(ReserveAssets.Count > 0)
        {
            int ReserveCount = ReserveAssets.Count;

            for(int Count = 0; Count < ReserveCount; Count++)
            {
                PlayMusic(Count);
            }
            ReserveAssets.Clear();
        }
        
    }   

    void PlayMusic(int count)
    {
        switch (ReserveAssets[count].AssetType)
        {
            case AssetTypeNumber.BGM:
                switch (ReserveAssets[count].AssetNumber)
                {
                    case 0:
                        break;

                    default:
                        break;
                }
                break;


            case AssetTypeNumber.SE:
                switch(ReserveAssets[count].AssetNumber)
                {
                    case ActionNumber.Umbrella:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[0]);
                        break;

                    case ActionNumber.Recorder:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[1]);
                        break;

                    case ActionNumber.Eraser:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[2]);
                        break;

                    case ActionNumber.Sacrifice:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[3]);
                        break;

                    case ActionNumber.AirCannon:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[4]);
                        break;

                    case ActionNumber.Bag:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[5]);
                        break;

                    case ActionNumber.Ruler:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[6]);
                        break;

                    case ActionNumber.Whistle:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[7]);
                        break;

                    case ActionNumber.None:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[8]);
                        break;

                    default:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[8]);
                        break;

                }
                break;

            default:
                break;

        }
    }

    public void AddMusicReserve(AssetTypeNumber type, ActionNumber number)
    {
        ReserveAsset Temporary = new ReserveAsset();
        Temporary.AssetType = type;
        Temporary.AssetNumber = number;
        ReserveAssets.Add(Temporary);
    }

    public Sprite ReferenceSprite(ActionNumber Action)
    {
        switch (Action)
        {
            case ActionNumber.Umbrella:
                return SpriteAsset[0];

            case ActionNumber.Recorder:
                return SpriteAsset[1];

            case ActionNumber.Eraser:
                return SpriteAsset[2];

            case ActionNumber.Sacrifice:
                return SpriteAsset[3];

            case ActionNumber.AirCannon:
                return SpriteAsset[4];

            case ActionNumber.Bag:
                return SpriteAsset[5];

            case ActionNumber.Ruler:
                return SpriteAsset[6];

            case ActionNumber.Whistle:
                return SpriteAsset[7];

            case ActionNumber.None:
                return SpriteAsset[8];

            default:
                return SpriteAsset[8];
        }
    }
}

