using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private ProgressBar progressBar;
    public AudioSource Source { get; set; }
    [SerializeField] private AudioClip resetSound;
    [SerializeField] private AudioClip thresholdSound;

    private const float thresholdSoundScale = 8f;

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
    private void Start()
    {
        progressBar = FindAnyObjectByType<ProgressBar>();
        progressBar.ThresholdReached += PlayThresholdSound;

    }

    private void PlayThresholdSound()
    {
        Source.PlayOneShot(thresholdSound, thresholdSoundScale);
    }

    public void ResumeTrack()
    {
        Source.Play();
    }
    public void PauseTrack()
    {
        Source.Pause();
        progressBar.PauseTracking();
    }

    public void ResetProgress(ProgressBar progressBar)
    {
        progressBar.ProgressSlider.value = 0;
        progressBar.progressText.text = "0%";
        Source.clip = null;
        Source.clip = TrackList.CurrentTrack.Clip;
    }

    public void PlayResetSound()
    {
        Source.PlayOneShot(resetSound);
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