using UnityEngine;
using TMPro;
using NUnit.Framework;

public class Score : MonoBehaviour
{
    public int ScoreCount { get; private set; }
    public int UniqueCount { get; private set; }

    public static bool WasScoreChanged { get; set; }

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

    public void AddScore()
    {
        for (int i = 0; i < ProgressBar.wereStarsOpened.Count; i++)
        {
            // Добавляем очки только за новые звёзды
            if (ProgressBar.wereStarsOpened[i] && !TrackList.CurrentTrack.starsOpenedEarlier[i])
            {
                ScoreCount++;
                MySaver.Instance.scoreCount++;
                TrackList.CurrentTrack.starsOpenedEarlier[i] = true; // Помечаем как открытую
            }
        }
    }

    public void AddUniqueScore()
    {
        if (!TrackList.CurrentTrack.UniqueCompleted)
        {
            UniqueCount++;
            MySaver.Instance.uniqueCount++;
            TrackList.CurrentTrack.UniqueCompleted = true;
        }
    }
}
