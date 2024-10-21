using System.Collections;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    [SerializeField] private Animator gifAnimator;
    [SerializeField] private Animator[] confettiAnims;
    private bool isActive;

    void Start()
    {
        gifAnimator.enabled = false;
    }
    public void StartAnimation()
    {
        gifAnimator.enabled = true;

        // restart animation
        if (!isActive)
        {
            gifAnimator.Play("Default", -1, 0f);  
            isActive = true;
        }
    }

    public void PauseAnimation()
    {
        gifAnimator.enabled = false;
    }

    public void PlayConfetti()
    {
        foreach (var confettiAnimator in confettiAnims)
        {
            confettiAnimator.Play("NotesVictory", -1, 0);
        }
    }
}
