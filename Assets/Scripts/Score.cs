using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;
    private int uniqueCount;

    public static bool ScoreChanged { get; set; }

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI UniqueText;

    public void WriteScoreText()
    {
        ScoreText.text = score.ToString();

        UniqueText.text = $"{uniqueCount}/30";
    }

    public void AddScore(Track currentTrack)
    {
        if (!currentTrack.UniqueCompleted)
        {
            uniqueCount += 1;
            currentTrack.UniqueCompleted = true;
        }
        score += 1;
    }
}
