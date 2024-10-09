using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Game game;
    [SerializeField] private SoundManager soundManager;

    private void Start()
    {
        backButton.onClick.AddListener(game.SwitchCanvas);
        backButton.onClick.AddListener(soundManager.PauseTrack);
        backButton.onClick.AddListener(() => soundManager.ResetProgress(game.TrackListGeneral, game.ProgressBar));
    }
}
