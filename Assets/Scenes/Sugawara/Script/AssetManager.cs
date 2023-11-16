using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public enum  ActionName               //アクション名
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
    [SerializeField] public GameObject[] PrefabObject = default;   //プレハブ化したオブジェクトの配列
    [SerializeField] private AudioClip[] SEAsset = default;       //格納するSEの配列
    [SerializeField] private AudioClip[] BGMAsset = default;      //格納するBGMの配列
    [SerializeField] private float AudioVolume = 0.0f;
    private AudioSource audioSource = null;     //音鳴らすための物
    
    public class ReserveAsset               //音鳴らすために予約をするクラス
    {
        public AssetTypeNumber AssetType;
        public ActionName AssetNumber;
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
        //AddMusicReserve(AssetTypeNumber.BGM, ActionName.AirCannon);
        //AddMusicReserve(AssetTypeNumber.SE, ActionName.Bag);
        //AddMusicReserve(AssetTypeNumber.SE, ActionName.Ruler);
        //AddMusicReserve(AssetTypeNumber.BGM, ActionName.Sacrifice);
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
                    case ActionName.Umbrella:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[0]);
                        break;

                    case ActionName.Recorder:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[1]);
                        break;

                    case ActionName.Eraser:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[2]);
                        break;

                    case ActionName.Sacrifice:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[3]);
                        break;

                    case ActionName.AirCannon:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[4]);
                        break;

                    case ActionName.Bag:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[5]);
                        break;

                    case ActionName.Ruler:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[6]);
                        break;

                    case ActionName.Whistle:
                        audioSource.volume = AudioVolume;
                        audioSource.PlayOneShot(SEAsset[7]);
                        break;

                    case ActionName.None:
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

    public void AddMusicReserve(AssetTypeNumber type, ActionName number)
    {
        ReserveAsset Temporary = new ReserveAsset();
        Temporary.AssetType = type;
        Temporary.AssetNumber = number;
        ReserveAssets.Add(Temporary);
    }

    public Sprite ReferenceSpriteBox(ActionName Action)
    {
        switch (Action)
        {
            case ActionName.Umbrella:
                return SpriteAsset[0];

            case ActionName.Recorder:
                return SpriteAsset[1];

            case ActionName.Eraser:
                return SpriteAsset[2];

            case ActionName.Sacrifice:
                return SpriteAsset[3];

            case ActionName.AirCannon:
                return SpriteAsset[4];

            case ActionName.Bag:
                return SpriteAsset[5];

            case ActionName.Ruler:
                return SpriteAsset[6];

            case ActionName.Whistle:
                return SpriteAsset[7];

            case ActionName.None:
                return SpriteAsset[8];

            default:
                return SpriteAsset[8];
        }
    }

    public Sprite ReferenceBox(RhythmManager.RhythmAction Action)
    {
        switch (Action)
        {
            case RhythmManager.RhythmAction.Umbrella:
                return SpriteAsset[0];

            case RhythmManager.RhythmAction.Recorder:
                return SpriteAsset[1];

            case RhythmManager.RhythmAction.Eraser:
                return SpriteAsset[2];

            case RhythmManager.RhythmAction.Sacrifice:
                return SpriteAsset[3];

            case RhythmManager.RhythmAction.AirCannon:
                return SpriteAsset[4];

            case RhythmManager.RhythmAction.Bag:
                return SpriteAsset[5];

            case RhythmManager.RhythmAction.Ruler:
                return SpriteAsset[6];

            case RhythmManager.RhythmAction.Whistle:
                return SpriteAsset[7];

            case RhythmManager.RhythmAction.None:
                return SpriteAsset[8];

            default:
                return SpriteAsset[8];
        }
    }

    public Sprite ReferenceIcon(ActionName Action)
    {
        if(SpriteAsset.Length > 9)
        {
            switch(Action)
            {
                case ActionName.Umbrella:
                    return SpriteAsset[9];

                case ActionName.Recorder:
                    return SpriteAsset[10];

                case ActionName.Eraser:
                    return SpriteAsset[11];

                case ActionName.Sacrifice:
                    return SpriteAsset[12];

                case ActionName.AirCannon:
                    return SpriteAsset[13];

                case ActionName.Bag:
                    return SpriteAsset[14];

                case ActionName.Ruler:
                    return SpriteAsset[15];

                case ActionName.Whistle:
                    return SpriteAsset[16];

                case ActionName.None:
                    return SpriteAsset[8];

                default:
                    return SpriteAsset[8];
            }
        }
        else
        {
            return SpriteAsset[8];
        }
        
    }

    public Sprite ReferenceFlavor(RhythmManager.RhythmAction Action)
    {
       if (SpriteAsset.Length > 16)
       {
            switch (Action)
            {
                case RhythmManager.RhythmAction.Umbrella:
                    return SpriteAsset[17];

                case RhythmManager.RhythmAction.Recorder:
                    return SpriteAsset[18];

                case RhythmManager.RhythmAction.Eraser:
                    return SpriteAsset[19];

                case RhythmManager.RhythmAction.Sacrifice:
                    return SpriteAsset[20];

                case RhythmManager.RhythmAction.AirCannon:
                    return SpriteAsset[21];

                case RhythmManager.RhythmAction.Bag:
                    return SpriteAsset[22];

                case RhythmManager.RhythmAction.Ruler:
                    return SpriteAsset[23];

                case RhythmManager.RhythmAction.Whistle:
                    return SpriteAsset[24];

                case RhythmManager.RhythmAction.None:
                    return SpriteAsset[25];

                default:
                    return SpriteAsset[25];
            }
        }
        else
        {
            return SpriteAsset[8];
        }
    }

    public Sprite ReferenceHeader(RhythmManager.RhythmAction Action)
    {
        if (SpriteAsset.Length > 16)
        {
            switch (Action)
            {
                case RhythmManager.RhythmAction.Umbrella:
                    return SpriteAsset[26];

                case RhythmManager.RhythmAction.Recorder:
                    return SpriteAsset[27];

                case RhythmManager.RhythmAction.Eraser:
                    return SpriteAsset[28];

                case RhythmManager.RhythmAction.Sacrifice:
                    return SpriteAsset[29];

                case RhythmManager.RhythmAction.AirCannon:
                    return SpriteAsset[30];

                case RhythmManager.RhythmAction.Bag:
                    return SpriteAsset[31];

                case RhythmManager.RhythmAction.Ruler:
                    return SpriteAsset[32];

                case RhythmManager.RhythmAction.Whistle:
                    return SpriteAsset[33];

                case RhythmManager.RhythmAction.None:
                    return SpriteAsset[34];

                default:
                    return SpriteAsset[34];
            }
        }
        else
        {
            return SpriteAsset[8];
        }
    }

}

