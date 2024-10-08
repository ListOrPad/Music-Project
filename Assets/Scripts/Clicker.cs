using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Clicker : MonoBehaviour
{
    private GameObject clicker;
    public GameObject Mask { get; private set; }
    private Image pic;

    private void Start()
    {
        clicker = GetComponent<GameObject>();
        Mask = GameObject.Find("ClickerMask");
        
    }

    private void Update()
    {
        if (TrackList.TrackChanged)
        {
            clicker = GetComponent<GameObject>();
            TrackList.TrackChanged = false;
        }
    }
    private void Twitch()
    {
        gameObject.transform.localPosition = new Vector2(10, 10);

    }
}
