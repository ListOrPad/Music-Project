using UnityEngine;
using YG;
using YG.Utils.LB;

public class Leaderboard : MonoBehaviour
{
    private const string MainLBName = "Score";
    private int currentScore;
    private int prevScore;

    private void Start()
    {
        YandexGame.onGetLeaderboard += RecallPrevScore;

        currentScore = MySaver.Instance.scoreCount;
    }

    private void Update()
    {
        if (Score.ScoreChanged)
        {
            currentScore = MySaver.Instance.scoreCount;
        }
        if(prevScore < currentScore)
        {
            writeScoreLb();
        }
    }

    private void writeScoreLb()
    {
        prevScore = currentScore;
        Debug.Log(currentScore + "is your NEW record");
        YandexGame.NewLeaderboardScores(MainLBName, currentScore);
    }

    private void RecallPrevScore(LBData lbData)
    {
        string currentPlayerId = YandexGame.playerId;

        // Find the current player's data in the leaderboard
        foreach (var player in lbData.players)
        {
            if (player.name == currentPlayerId)
            {
                prevScore = player.score;
                break;
            }
        }
    }
}
