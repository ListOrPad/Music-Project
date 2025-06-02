using UnityEngine;

public class Bookmark : MonoBehaviour
{
    public bool Selected { get; set; }
    public RectTransform rectTransform { get; set; }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
