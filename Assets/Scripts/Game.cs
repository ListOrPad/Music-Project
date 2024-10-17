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

    private void Start()
    {
        TrackListGeneral.PrepareTracklistButtons(this, clicker);
        clicker.ClickerButton.onClick.AddListener(() => clicker.Click(this));
        Timer = 0f;
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if(AudSource.isPlaying)
        {
            ProgressBar.UpdateProgressBar(AudSource);
        }

        // pause if time is out
        if (Timer >= idleTime)
        {
            SoundManager.Instance.PauseTrack();
            BgAnimation.PauseAnimation();
        }

        //if Track is completed(progressbar is filled)
        if (ProgressBar.ProgressSlider.value >= 0.999)
        {
            BlockPlaying();
            if (!Score.ScoreChanged)
            {
                scoreObj.AddScore(TrackListGeneral.CurrentTrack);
                scoreObj.WriteScoreText();
                Score.ScoreChanged = true;
            }
            //what else should happen?

            //PlayConfettiGif();
            //ResetAdBlock() (if track wasnt completed)???

            //allow voting for mark
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
    

    #region rewarded ad
    // Subscribe to the ad opening event in OnEnable
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // Unsubscribe from the ad opening event in OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // Method subscribed to receive a reward
    private void Rewarded(int id)
    {
        if (id == 1)
        {
            //SkipExample()
        }
    }       

    // Method for calling video ads
    public void ExampleOpenRewardAd(int id)
    {
        // Call the method to open video ads
        YandexGame.RewVideoShow(id);
    }

    #endregion

}
