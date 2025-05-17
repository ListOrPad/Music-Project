using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    [field: SerializeField] public Slider ProgressSlider { get; set; }

    [field: SerializeField] public TextMeshProUGUI progressText { get; private set; }

    private void Start()
    {
        ProgressSlider.interactable = false;
    }

    public void UpdateProgress(AudioSource audioSource)
    {
        // Calculate the percentage of the played time relative to the total track length
        float progress = audioSource.time / audioSource.clip.length;
        ProgressSlider.value = progress;
        progressText.text = $"{(int)Math.Round(progress * 100)}%";
    }
}