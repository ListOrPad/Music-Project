using UnityEngine;

[System.Serializable]
public class Game : MonoBehaviour, IResettable
{
    [field:SerializeField] public AudioSource AudSource { get; set; }
    [field:SerializeField] public ProgressBar ProgressBar { get; set; }
    [field:SerializeField] public BackgroundAnimation BgAnimation { get; set; }
    [field:SerializeField] public TrackList TrackListGeneral { get; set; }

    [Header("Timers")]
    public float Timer { get; set; }
    [SerializeField] private float idleTime = 1f;   // time before pause
    public bool IsTimerRunning { get; set; }

    [Space(15)]

    [SerializeField] private Canvas gameProcessCanvas;
    [field: SerializeField] public Animator Anim { get; set; }
    [field: SerializeField] public Clicker clicker { get; set; }
    [SerializeField] public Score scoreObj;
    private Advertisment ad;
    public static int ClipSpeed { get; set; }

    private void Start()
    {
        TrackListGeneral.PrepareTracklistButtons(this, clicker);
        clicker.ClickerButton.onClick.AddListener(() => clicker.Click(this));
        ad = GetComponent<Advertisment>();

        scoreObj.WriteScoreText();

        Timer = 0f;
    }

    private void Update()
    {
        if(IsTimerRunning)
            Timer += Time.deltaTime;
        
        ProgressBar.UpdateProgress(AudSource);

        if (TrackList.CurrentTrackChanged)
        {
            UnlockBookmarks();
            ProgressBar.ResetProgressBar();

            TrackList.CurrentTrackChanged = false;
        }

        // pause if time is out
        if (Timer >= idleTime)
        {
            SoundManager.Instance.PauseTrack();
            BgAnimation.PauseAnimation();
        }

        //autoplay
        if (ClipSpeed == 4 && !TrackList.TrackFinished) 
        {
            clicker.Click(this);
        }

        OnTrackCompleted();

    }

    /// <summary>
    /// if Track is completed(progressbar is filled)
    /// </summary>
    public void OnTrackCompleted()
    {
        if (ProgressBar.ProgressSlider.value >= 0.996f)
        {
            IsTimerRunning = false;
            ProgressBar.ProgressSlider.value = 1f;
            if (!TrackList.TrackFinished)
            {
                BgAnimation.PlayConfetti();
                TrackList.TrackFinished = true;
            }

            BlockPlaying();

            //If score hadn't changed before
            if (!Score.WasScoreChanged)
            {
                scoreObj.AddUniqueScore();
                scoreObj.WriteScoreText();
                Score.WasScoreChanged = true;
            }

            //make voter appear
            gameObject.GetComponent<VoteSystem>().Appear();
        }
    }

    public void SwitchCanvas()
    {
        if (gameProcessCanvas.transform.GetSiblingIndex() == 1)
        {
            gameProcessCanvas.transform.SetSiblingIndex(0);
        }
        else
        {
            gameProcessCanvas.transform.SetSiblingIndex(1);
        }
    }

    private void BlockPlaying()
    {
        clicker.ClickerButton.onClick.RemoveAllListeners();
        SoundManager.Instance.PauseTrack();
        BgAnimation.PauseAnimation();
        Anim.ResetTrigger("Click"); //pause twitching
    }

    private void UnlockBookmarks()
    {
        if (TrackList.CurrentTrack.UniqueCompleted)
        {
            ad.AdLock.gameObject.SetActive(false);
        }
    }

    public void OnBackButtonReset()
    {
        SwitchCanvas();
        ClipSpeed = 0;
        clicker.ClickerButton.onClick.AddListener(() => clicker.Click(this));
    }
}
