using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int ScoreCount { get; private set; }
    public int UniqueCount { get; private set; }

    public static bool ScoreChanged { get; set; }

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI UniqueText;

    private void Start()
    {
        //load data from MySaver
        ScoreCount = MySaver.Instance.scoreCount;
        UniqueCount = MySaver.Instance.uniqueCount;
    }

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
            MySaver.Instance.uniqueCount += 1;
            currentTrack.UniqueCompleted = true;
        }
        ScoreCount += 1;
        MySaver.Instance.scoreCount += 1;
    }
}
