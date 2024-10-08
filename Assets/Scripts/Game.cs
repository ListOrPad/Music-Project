using UnityEngine;
using UnityEngine.UI;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private BackgroundAnimation BgAnimation;

    private float timer;
    [SerializeField] private float idleTime = 1.5f;   // time before pause

    [SerializeField] private Button clickButton;

    [SerializeField] private Canvas gameProcessCanvas;
    [SerializeField] private Canvas tracklistMenuCanvas; //not needed anymore?
    private TrackList trackList;
    private Clicker clicker;

    public void Click()
    {
        if (!audioSource.isPlaying)
        {
            SoundManager.Instance.PlayTrack();
        }
        if (audioSource.isPlaying)
        {
            progressBar.UpdateProgressBar(audioSource);
            BgAnimation.StartAnimation();
            timer = 0; //reset timer
        }
        //Twitch();
    }


    private void Awake()
    {
        trackList = GameObject.Find("TrackList").GetComponent<TrackList>();
        clicker = GameObject.Find("Click Button").GetComponent<Clicker>();
    }
    private void Start()
    {
        trackList.PrepareTracklistButtons(this, clicker);
        clickButton.onClick.AddListener(Click);
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // pause if time is out
        if (timer >= idleTime)
        {
            audioSource.Pause();
            BgAnimation.PauseAnimation();
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
