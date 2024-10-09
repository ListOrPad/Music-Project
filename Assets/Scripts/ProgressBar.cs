using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    public void UpdateProgressBar(AudioSource audioSource)
    {

        // Calculate the percentage of the played time relative to the total track length
        float progress = audioSource.time / audioSource.clip.length;
        progressBar.value = progress;
    }
}