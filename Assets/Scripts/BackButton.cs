using UnityEngine;
using UnityEngine.UI;
using YG;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Game game;  //this actually breaks the design, should better create backbutton instance in Game ?
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Advertisment ad;

    [SerializeField] private float secUntilAd = 50;

    private void Start()
    {
        backButton.onClick.AddListener(game.SwitchCanvas);
        backButton.onClick.AddListener(soundManager.PauseTrack);
        backButton.onClick.AddListener(() => soundManager.ResetProgress(game.TrackListGeneral, game.ProgressBar));
        backButton.onClick.AddListener(() => Reset(game));
        backButton.onClick.AddListener(YandexGame.FullscreenShow);
    }

    private void Reset(Game game)
    {
        Score.ScoreChanged = false;
        TrackList.TrackFinished = false;
        ad.AdLock.SetActive(true);
        Game.ClipSpeed = 0;
        SoundManager.Instance.Source.pitch = 1f;
        game.clicker.ClickerButton.onClick.AddListener(() => game.clicker.Click(game));
    }
}
