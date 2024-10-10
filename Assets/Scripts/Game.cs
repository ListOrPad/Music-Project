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
    [SerializeField] private float idleTime = 1.5f;   // time before pause

    [SerializeField] private Button clickButton;

    [SerializeField] private Canvas gameProcessCanvas;
    [SerializeField] private Canvas tracklistMenuCanvas; //not needed anymore?
    private Clicker clicker;



    private void Awake()
    {
        TrackListGeneral = GameObject.Find("TrackList").GetComponent<TrackList>();
        clicker = GameObject.Find("Click Button").GetComponent<Clicker>();
    }
    private void Start()
    {
        TrackListGeneral.PrepareTracklistButtons(this, clicker);
        clickButton.onClick.AddListener(() => clicker.Click(this));
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
