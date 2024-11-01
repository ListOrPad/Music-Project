using UnityEngine;
using UnityEngine.UI;
using YG;

[System.Serializable]
public class Game : MonoBehaviour
{
    [field:SerializeField] public AudioSource AudSource { get; set; }
    [field:SerializeField] public ProgressBar ProgressBar { get; set; }
    [field:SerializeField] public BackgroundAnimation BgAnimation { get; set; }
    [field:SerializeField] public TrackList TrackListGeneral { get; set; }
    
    public float Timer { get; set; }
    [SerializeField] private float idleTime = 1f;   // time before pause

    [SerializeField] private Canvas gameProcessCanvas;
    [field: SerializeField] public Animator Anim { get; set; }
    [field: SerializeField] public Clicker clicker { get; set; }
    [SerializeField] private Score scoreObj;
    private Advertisment ad;
    public static int ClipSpeed { get; set; }

    private void Start()
    {
        TrackListGeneral.PrepareTracklistButtons(this, clicker);
        clicker.ClickerButton.onClick.AddListener(() => clicker.Click(this));
        ad = GetComponent<Advertisment>();
        Timer = 0f;
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if(AudSource.isPlaying)
        {
            ProgressBar.UpdateProgress(AudSource);
        }

        if (TrackList.CurrentTrackChanged)
        {
            UnlockBookMarks();
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

        //if Track is completed(progressbar is filled)
        if (ProgressBar.ProgressSlider.value >= 0.995)
        {
            if (!TrackList.TrackFinished)
            {
                BgAnimation.PlayConfetti();
                TrackList.TrackFinished = true;
            }

            BlockPlaying();

            if (!Score.ScoreChanged)
            {
                scoreObj.AddScore(TrackListGeneral.CurrentTrack);
                scoreObj.WriteScoreText();
                Score.ScoreChanged = true;
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

    private void UnlockBookMarks()
    {
        if (TrackListGeneral.CurrentTrack.UniqueCompleted)
        {
            ad.AdLock.gameObject.SetActive(false);
        }
    }
}
