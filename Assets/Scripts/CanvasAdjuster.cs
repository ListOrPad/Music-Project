using UnityEngine;

public class CanvasAdjuster : MonoBehaviour
{
    void Start()
    {
        ResizeCanvas();
    }

    void Update()
    {
        ResizeCanvas();
    }

    void ResizeCanvas()
    {
        int width = Mathf.RoundToInt(Screen.width);
        int height = Mathf.RoundToInt(Screen.height);
        Screen.SetResolution(width, height, false);
    }
}
