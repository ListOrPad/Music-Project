using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;
    private int perfectsScore;

    public static bool ScoreChanged { get; set; }

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI PerfectsText;

    public void WriteScoreText()
    {
        ScoreText.text = score.ToString();

        PerfectsText.text = $"{perfectsScore}/30";
    }

    public void AddScore(Track currentTrack)
    {
        if (!currentTrack.Completed)
            perfectsScore += 1;
        score += 1;
    }
}
