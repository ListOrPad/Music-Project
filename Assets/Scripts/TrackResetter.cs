using UnityEngine;

public class TrackResetter : MonoBehaviour
{
    [SerializeField] private Game game;

    private const float timeToPBarReset = 3f; //time before progress bar reset

    private void Update()
    {
        if (game.Timer > timeToPBarReset)
        {
            ResetTrackProgress();
        }
    }

    private void ResetTrackProgress()
    {
        game.AudSource.time = 0f;
        game.AudSource.Stop();
        game.ProgressBar.ResetProgressBar();
        SoundManager.Instance.PlayResetSound();
        game.Timer = 0f;
        game.IsTimerRunning = false;
    }


}
