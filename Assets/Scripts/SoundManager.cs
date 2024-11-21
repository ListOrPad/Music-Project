using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource Source { get; set; }

    [SerializeField] private TrackList tracks;


    private void Awake()
    {
        Source = GetComponent<AudioSource>();

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
        Source.PlayOneShot(clip);
    }

    public void PlayTrack()
    {
        PlaySound(tracks.CurrentTrack.Clip);
    }

    public void ResumeTrack()
    {
        Source.Play();
    }
    public void PauseTrack()
    {
        Source.Pause();
    }

    public void ResetProgress(TrackList trackList, ProgressBar progressBar)
    {
        progressBar.ProgressSlider.value = 0;
        progressBar.progressText.text = "0%";
        Source.clip = null;
        Source.clip = trackList.CurrentTrack.Clip;
    }

    public void ChangeSpeed(int speed)
    {
        Game.ClipSpeed = speed;
        if (Game.ClipSpeed == (int)AudioSpeed.Normal || Game.ClipSpeed == (int)AudioSpeed.Auto)
        {
            Source.pitch = 1f;
        }
        else if (Game.ClipSpeed == (int)AudioSpeed.Fast)
        {
            Source.pitch = 1.5f;
        }
        else if (Game.ClipSpeed == (int)AudioSpeed.Faster)
        {
            Source.pitch = 2f;
        }
        else if (Game.ClipSpeed == (int)AudioSpeed.Fastest)
        {
            Source.pitch = 3f;
        }
        
    }

}