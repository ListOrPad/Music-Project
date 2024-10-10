using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [field: SerializeField] public Slider ProgressSlider { get; set; }

    public void UpdateProgressBar(AudioSource audioSource)
    {

        // Calculate the percentage of the played time relative to the total track length
        float progress = audioSource.time / audioSource.clip.length;
        ProgressSlider.value = progress;
    }
}