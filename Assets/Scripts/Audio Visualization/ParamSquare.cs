using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamSquare : MonoBehaviour
{
    [SerializeField] private int band;
    [SerializeField] private float startScale, scaleMultiplier;
    [SerializeField] private bool useBuffer;

    private void Update()
    {
        if (useBuffer)
            transform.localScale = new Vector2(transform.localScale.x, (AudioVisualizer.bandBuffer[band] * scaleMultiplier) + startScale);
        if (!useBuffer)
            transform.localScale = new Vector2(transform.localScale.x, (AudioVisualizer.freqBand[band] * scaleMultiplier) + startScale);
    }

}
