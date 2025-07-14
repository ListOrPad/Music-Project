using UnityEngine;
using UnityEngine.UI;
using YG;

public class ResetManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Game game;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private BookmarkManager bookmarkManager;
    [SerializeField] private Advertisment ad;
    [SerializeField] private VoteSystem voteSystem;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TrackList trackList;

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
        //reset
        Score.WasScoreChanged = false;
        TrackList.TrackFinished = false;
        game.SwitchCanvas();
        ad.AdLock.SetActive(true);
        Game.ClipSpeed = 0;
        soundManager.PauseTrack();
        SoundManager.Instance.Source.pitch = 1f;
        soundManager.ResetProgress(game.ProgressBar);
        bookmarkManager.ResetToFirst();
        game.clicker.ClickerButton.onClick.AddListener(() => game.clicker.Click(game));

        foreach (var image in progressBar.StarImages)
        {
            image.color = new Color32(0, 0, 0, 255);
        }


        //analyses opened stars on reset
        for (int i = 0; i < ProgressBar.wereStarsOpened.Count; i++)
        {
            if (!TrackList.CurrentTrack.starsOpenedEarlier[i]) //if star was opened earlier we don't set it true
                TrackList.CurrentTrack.starsOpenedEarlier[i] = ProgressBar.wereStarsOpened[i];
        }

        SaveStarsState();

        //resets stars, opened during session
        for (int i = 0; i <= 2; i++)
        {
            ProgressBar.wereStarsOpened[i] = false;
        }
    }

    /// <summary>
    /// Save track star states for the future
    /// </summary>
    public void SaveStarsState()
    {
        for (int i = 0; i < trackList.TrackObjects.Count; i++)
        {
            MySaver.Instance.starsOpenedEarlierArray[i] = trackList.TrackObjects[i].starsOpenedEarlier;
        }
    }

}
