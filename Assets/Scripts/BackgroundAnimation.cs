using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    [SerializeField] private Animator gifAnimator;
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
}
