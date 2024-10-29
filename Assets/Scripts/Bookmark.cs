using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bookmark : MonoBehaviour
{
    public bool Selected { get; set; }

    public RectTransform Transform { get; set; }

    private void Start()
    {
        Transform = GetComponent<RectTransform>();
    }
}
