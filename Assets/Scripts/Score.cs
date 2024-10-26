using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int ScoreCount { get; private set; }
    public int UniqueCount { get; private set; }

    public static bool ScoreChanged { get; set; }

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI UniqueText;

    public void WriteScoreText()
    {
        ScoreText.text = ScoreCount.ToString();

        UniqueText.text = $"{UniqueCount}/30";
    }

    public void AddScore(Track currentTrack)
    {
        if (!currentTrack.UniqueCompleted)
        {
            UniqueCount += 1;
            currentTrack.UniqueCompleted = true;
        }
        ScoreCount += 1;
    }
}
