using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;
    private int perfectsScore;

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI PerfectsText;

    public void WriteScoreText()
    {
        ScoreText.text = score.ToString();

        PerfectsText.text = $"{perfectsScore.ToString()}/30";
    }
}
