using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private AudioSource audioSource;

    public void UpdateProgressBar(AudioSource audioSource)
    {
        // Calculate the percentage of the played time relative to the total track length
        float progress = audioSource.time / audioSource.clip.length;
        progressBar.value = progress; // Update the progress bar value
    }
}