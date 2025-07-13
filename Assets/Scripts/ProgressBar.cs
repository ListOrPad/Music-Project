using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ProgressBar : MonoBehaviour
{
    public event Action ThresholdReached;

    [field: SerializeField] public Slider ProgressSlider { get; set; }
    [field: SerializeField] public TextMeshProUGUI progressText { get; private set; }

    [field: SerializeField] public List<Image> StarImages { get; set; }

    public static List<bool> wereStarsOpened = new List<bool>() { false, false, false };
    [SerializeField] private Score scoreObj;

    private float accumulatedTime = 0f;
    private bool isPlaying = false;

    private bool isThresholdReached = false;

    private void Start()
    {
        ProgressSlider.interactable = false;
        ThresholdReached += OpenStars;
        ThresholdReached += scoreObj.AddScore;
        ThresholdReached += scoreObj.WriteScoreText;
    }

    private void Update()
    {
        float value = ProgressSlider.value;
        float epsilon = 0.001f; //permissable error

        if (Mathf.Abs(value - 0.33f) < epsilon ||
            Mathf.Abs(value - 0.66f) < epsilon ||
            Mathf.Abs(value - 0.995f) < epsilon)
        {
            if (!isThresholdReached)
            {

               

                isThresholdReached = true;
                ThresholdReached();
            }
        }
        else
        {
            isThresholdReached = false;
        }
    }

    public void StartTracking()
    {
        isPlaying = true;
    }

    public void PauseTracking()
    {
        isPlaying = false;
    }

    public void UpdateProgress(AudioSource audioSource)
    {
        if (audioSource.clip == null) return;
        if (!isPlaying) return;

        accumulatedTime += Time.unscaledDeltaTime * audioSource.pitch;

        float progress = Mathf.Clamp01(accumulatedTime / audioSource.clip.length);

        ProgressSlider.value = progress;
        progressText.text = $"{(int)(progress * 100)}%";
        if (ProgressSlider.value > 0.996)
        {
            progressText.text = "100%";
        }
    }
    public void ResetProgress()
    {
        accumulatedTime = 0f;
        ProgressSlider.value = 0;
        progressText.text = "0%";

        isPlaying = false;
    }

    public void OpenStars()
    {
        float value = ProgressSlider.value;
        float epsilon = 0.001f; //permissable error

        if (Mathf.Abs(value - 0.33f) < epsilon || TrackList.CurrentTrack.starsOpenedEarlier[0])
            wereStarsOpened[0] = true;
        if (Mathf.Abs(value - 0.66f) < epsilon || TrackList.CurrentTrack.starsOpenedEarlier[1])
            wereStarsOpened[1] = true;
        if (Mathf.Abs(value - 0.995f) < epsilon || TrackList.CurrentTrack.starsOpenedEarlier[2])
            wereStarsOpened[2] = true;

        for (int i = 0; i < wereStarsOpened.Count; i++)
        {
            if (wereStarsOpened[i])
                StarImages[i].color = new Color32(255, 255, 255, 215);
            Debug.Log($"Star {i} state: {wereStarsOpened[i]}, Color: {StarImages[i].color}");
        }
    }
}