using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    private GameObject clicker;
    //pic section
    [SerializeField] private GameObject clickerPic;
    public Sprite Pic {get; set; }

    private void Start()
    {
        clicker = gameObject;
    }

    private void Update()
    {
        if (TrackList.TrackChanged)
        {
            clickerPic.GetComponent<Image>().sprite = Pic;
            clicker = gameObject;
            TrackList.TrackChanged = false;
        }
    }
    private void Twitch()
    {
        gameObject.transform.localPosition = new Vector2(10, 10);

    }
}
