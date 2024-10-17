using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Game game;  //this actually breaks the design, should better create backbutton instance in Game ?
    [SerializeField] private SoundManager soundManager;

    private float adTimer;

    private void Start()
    {
        backButton.onClick.AddListener(game.SwitchCanvas);
        backButton.onClick.AddListener(soundManager.PauseTrack);
        backButton.onClick.AddListener(() => soundManager.ResetProgress(game.TrackListGeneral, game.ProgressBar));
        backButton.onClick.AddListener(() => ResetBlock(game));
    }

    private void ResetBlock(Game game)
    {
        Score.ScoreChanged = false;
        game.clicker.ClickerButton.onClick.AddListener(() => game.clicker.Click(game));
    }

    private void Update()
    {
        adTimer += Time.deltaTime;
        if (adTimer > 50)
        {
            //show ad here!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            adTimer = 0;
        }
    }
}
