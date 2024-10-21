using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Game game;  //this actually breaks the design, should better create backbutton instance in Game ?
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Advertisment ad;

    [SerializeField] private float secUntilAd = 50;
    private float adTimer;

    private void Start()
    {
        backButton.onClick.AddListener(game.SwitchCanvas);
        backButton.onClick.AddListener(soundManager.PauseTrack);
        backButton.onClick.AddListener(() => soundManager.ResetProgress(game.TrackListGeneral, game.ProgressBar));
        backButton.onClick.AddListener(() => UndoBlock(game));
    }

    private void UndoBlock(Game game)
    {
        Score.ScoreChanged = false;
        TrackList.TrackFinished = false;
        ad.AdLock.SetActive(true);
        Game.ClipSpeed = 0;
        SoundManager.Instance.Source.pitch = 1f;
        game.clicker.ClickerButton.onClick.AddListener(() => game.clicker.Click(game));
    }

    private void Update()
    {
        adTimer += Time.deltaTime;
        if (adTimer > secUntilAd)
        {
            //show ad here!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            adTimer = 0;
        }
    }
}
