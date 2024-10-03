using UnityEngine;
using UnityEngine.UI;
using YG;

public class Game : MonoBehaviour
{
    private AudioTrack track;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private BackgroundAnimation BgAnimation;

    private float timer;
    [SerializeField] private float idleTime = 1.5f;   // time before pause

    [SerializeField] private Button clickButton;


    public void Click()
    {
        if (!audioSource.isPlaying)
        {
            track.PlayTrack();
        }
        if (audioSource.isPlaying)
        {
            progressBar.UpdateProgressBar(audioSource);
            BgAnimation.StartAnimation();
            timer = 0; //reset timer
        }
        //Twitch();
    }

    private void Start()
    {
        clickButton.onClick.AddListener(Click);
        timer = 0f;

    }

    private void Update()
    {
        timer += Time.deltaTime;

        // pause if time is out
        if (timer >= idleTime)
        {
            BgAnimation.PauseAnimation();
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
