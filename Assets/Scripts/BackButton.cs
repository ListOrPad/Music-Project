using UnityEngine;
using UnityEngine.UI;
using YG;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Game game;  //this actually breaks the design, should better create backbutton instance in Game ?
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private BookmarkManager bookmarkManager;
    [SerializeField] private Advertisment ad;

    private void Start()
    {
        backButton.onClick.AddListener(ResetGame);
        backButton.onClick.AddListener(YandexGame.FullscreenShow);
    }

    /// <summary>
    /// Resets the game on return to tracklist
    /// </summary>
    private void ResetGame()
    {
        Score.ScoreChanged = false;
        TrackList.TrackFinished = false;
        game.SwitchCanvas();
        ad.AdLock.SetActive(true);
        Game.ClipSpeed = 0;
        soundManager.PauseTrack();
        SoundManager.Instance.Source.pitch = 1f;
        soundManager.ResetProgress(game.TrackListGeneral, game.ProgressBar);
        bookmarkManager.ResetToFirst();
        game.clicker.ClickerButton.onClick.AddListener(() => game.clicker.Click(game));
    }
}
