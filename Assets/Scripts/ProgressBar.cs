using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    [field: SerializeField] public Slider ProgressSlider { get; set; }
    [field: SerializeField] public TextMeshProUGUI progressText { get; private set; }

    private float accumulatedTime = 0f;
    private bool isPlaying = false;

    private void Start()
    {
        ProgressSlider.interactable = false;
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
}