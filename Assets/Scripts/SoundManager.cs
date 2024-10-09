using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource source;

    [SerializeField] private TrackList tracks;


    private void Awake()
    {
        source = GetComponent<AudioSource>();

        //Keep this object even when we go to a new scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //destroy duplicate gameobjects
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayTrack()
    {
        PlaySound(tracks.CurrentTrack);
    }

    public void ResumeTrack()
    {
        source.Play();
    }
    public void PauseTrack()
    {
        source.Pause();
    }

    public void ResetProgress(TrackList trackList, ProgressBar progressBar)
    {
        progressBar.ProgressSlider.value = 0;
        source.clip = null;
        source.clip = trackList.CurrentTrack;
    }

    public void ChangeSpeed(int speed)
    {
        if (speed == (int)AudioSpeed.Normal || speed == (int)AudioSpeed.Auto)
        {
            source.pitch = 1f;
        }
        else if (speed == (int)AudioSpeed.Fast)
        {
            source.pitch = 1.5f;
        }
        else if (speed == (int)AudioSpeed.Faster)
        {
            source.pitch = 2f;
        }
        else if (speed == (int)AudioSpeed.Fastest)
        {
            source.pitch = 3f;
        }
    }

}