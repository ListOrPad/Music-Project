using UnityEngine;
using UnityEngine.UI;
using YG;

public class ResetManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Game game;
    [SerializeField] private Advertisment ad;
    [SerializeField] private VoteSystem voteSystem;
    [SerializeField] private TrackList trackList;
    [SerializeField] private ResetService resetService;


    private void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClick);
        backButton.onClick.AddListener(YandexGame.FullscreenShow);
    }

    /// <summary>
    /// Resets the game on return to tracklist
    /// </summary>
    private void OnBackButtonClick()
    {
        //reset
        resetService.ResetAll();
        Score.WasScoreChanged = false;
        TrackList.TrackFinished = false;
        ad.AdLock.SetActive(true);

        //reset this SESSION star colors
        foreach (var image in game.ProgressBar.StarImages)
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
