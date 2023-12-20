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

public class RankingDraw_1_3 : MonoBehaviour
{
    [SerializeField]
    private Text scoreObject = default;
    [SerializeField]
    private Text nameObject = default;
  

    // Start is called before the first frame update
    void Start()
    {
        GetLeaderboard();
    }

    // ランキング情報の取得
    public void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,    // 取得する開始位置
            MaxResultsCount = 3 // 最大取得数
        },
        (result) =>
        {
            // 取得したランキング情報
            if (result?.Leaderboard != null)
            {
                Text scoreText = scoreObject.GetComponent<Text>();
                Text nameText = nameObject.GetComponent<Text>();
               
                scoreText.text = "";
                nameText.text = "";
               

                for (var i = 0; i < result.Leaderboard.Count; i++)
                {
                    var entry = result.Leaderboard[i];
                    scoreText.text += $"\n{entry.StatValue}";
                    nameText.text += $"\n{entry.DisplayName}";
                   
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
}
