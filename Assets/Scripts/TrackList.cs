using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackList : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioTracks;
    [SerializeField] private List<GameObject> trackObjects;
    public AudioClip CurrentTrack { get; private set; }
    public static bool TrackChanged { get; set; }

    public void PrepareTracklistButtons(Game game, Clicker clicker)
    {
        for (int i = 0; i <= trackObjects.Count - 1 ; i++)
        {
            GameObject trackObject = trackObjects[i];
            Button button = trackObject.GetComponentInChildren<Button>();

            button.onClick.AddListener(game.SwitchCanvas);
            button.onClick.AddListener(() => ChangeData(clicker, trackObject)); //change elements in playmode
        }
    }

    public void SetNewTrack(int chosenTrackID)
    {
        for (int i = 0; i <= audioTracks.Count; i++)
        {
            if (chosenTrackID == i)
            {
                CurrentTrack = audioTracks[i];
                return;
            }
        }

        Debug.LogError("Error finding a track");
    }

    /// <summary>
    /// change elements in playmode
    /// </summary>
    private void ChangeData(Clicker clicker, GameObject trackObject)   //do I need this clicker param?
    {
        TrackChanged = true;

        //get pic from tracklist item
        Transform mask = trackObject.transform.Find("mask");
        Transform picTransform = mask.transform.Find("Pic");
        Image pic = picTransform.GetComponent<Image>();
        clicker.Pic = pic.sprite; //finally set pic\
        clicker.ClickerPic.GetComponent<Image>().sprite = clicker.Pic;
        //set current track to source clip
        AudioSource source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        source.clip = CurrentTrack;

    }
}
