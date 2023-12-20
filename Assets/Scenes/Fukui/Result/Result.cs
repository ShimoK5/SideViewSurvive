using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;
using System.Text;
using System.Threading.Tasks;

public class Result : MonoBehaviour
{
    private const string TitleId = "3208A";
    public int ResultScore;
    public static int RankCount; 

    // Start is called before the first frame update
    void Start()
    {
        var customID = GenerateCustomID();

        StartCoroutine(DelayLoginMethod(1.0f, customID));

        ResultScore = ScoreManager.instance.m_Score;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") /*|| Input.GetButtonDown("Action1")*/) //スペースキー、Aボタン
        {
            GetLeaderboard();
        }
    }

    //ユーザーID生成
    private static readonly string characterId = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmmnopqrstuvwxyz";
    private string GenerateCustomID()
    {
        int idLength = 16;
        StringBuilder stringBuilder = new StringBuilder(idLength);
        var random = new System.Random();

        //IDランダム生成
        for (int i = 0; i < idLength; i++)
        {
            stringBuilder.Append(characterId[random.Next(characterId.Length)]);
        }

        return stringBuilder.ToString();
    }

    //Coroutine for delay method
    private IEnumerator DelayLoginMethod(float waitTime, string customID)
    {
        yield return new WaitForSeconds(waitTime);
        Login(customID);
    }

    //ログイン処理
    public void Login(string customID)
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = TitleId;
        }

        // リクエスト生成
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customID,
            CreateAccount = true // アカウントを作成するか？
        };

        PlayFabClientAPI.LoginWithCustomID(request,
        (result) =>
        {
            Debug.Log("Create Account Success!!");
        },
        (error) =>
        {
            Debug.LogError("Create Account Failed...");
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    //スコア送信
    public void SubmitScore(int playerScore)
    {
        // スコア送信実行
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = playerScore // スコア
                }
            }
        },
        (result) => // 送信成功
        {
            Debug.Log("Send Ranking Score Success!!");
        },
        (error) => // 送信失敗
        {
            Debug.LogError("Send Ranking Score Failed...");
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    // ランキング情報の取得
    public void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "HighScore", //ランキング名
            StartPosition = 0,    // 取得する開始位置
            MaxResultsCount = 10 // 最大取得数
        },
        (result) =>
        {
            // 取得したランキング情報
            if (result?.Leaderboard != null)
            {
                for (var i = 0; i < result.Leaderboard.Count; i++)
                {
                    var entry = result.Leaderboard[i];
                    RankCount = i;
                    
                    
                    if (ResultScore > entry.StatValue)
                    {
                        SubmitScore(ResultScore);
                        Invoke(nameof(DelayNameInput), 1.0f);
                        break;
                    }
                    
                    if(i == result.Leaderboard.Count - 1 && ResultScore <= entry.StatValue)
                    {
                        Invoke(nameof(DelayDrawRanking), 1.0f);
                    }
                }
            }


            Debug.Log("Get Leader Board Success!!");
        },
        (error) =>
        {
            Debug.LogError("Get Leader Board Failed...");
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    private void DelayDrawRanking()
    {
        SceneChangeManager.instance.SceneTransition("DrawRanking");
    }

    private void DelayNameInput()
    {
        SceneChangeManager.instance.SceneTransition("NameInput");
    }
}
