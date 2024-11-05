using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackList : MonoBehaviour
{
    [SerializeField] private List<Track> trackObjects;
    public Track CurrentTrack { get; private set; }
    public static bool CurrentTrackChanged { get; set; }
    public static bool TrackFinished { get; set; }

    [SerializeField] private VoteSystem voteSystem;


    private void Start()
    {
        //load data from MySaver
        for (int i = 0; i < trackObjects.Count; i++)
        {
            Track track = trackObjects[i];
            track.UniqueCompleted = MySaver.Instance.uniquesCompleted[i];
            track.VoteUp = MySaver.Instance.votesUp[i];
            track.ActivateVote();
        }

        LightTrackItems();
    }
    private void Update()
    {
        if (TrackFinished)
        {
            for (int i = 0; i < trackObjects.Count; i++)
            {
                //let uniques completed be saved in MySaver
                Track track = trackObjects[i];
                if (track.UniqueCompleted)
                {
                    MySaver.Instance.uniquesCompleted[i] = track.UniqueCompleted;
                }
            }

            LightTrackItems();
        }

        if (voteSystem.VoteChanged)
        {
            for (int i = 0; i < trackObjects.Count; i++)
            {
                //saves votes in MySaver
                Track track = trackObjects[i];
                MySaver.Instance.votesUp[i] = track.VoteUp;
            }

            CurrentTrack.ActivateVote();
            voteSystem.VoteChanged = false;
        }
    }

    public void PrepareTracklistButtons(Game game, Clicker clicker)
    {
        for (int i = 0; i < trackObjects.Count; i++)
        {
            GameObject trackObject = trackObjects[i].gameObject;
            Button button = trackObject.GetComponentInChildren<Button>();

            button.onClick.AddListener(game.SwitchCanvas);
            button.onClick.AddListener(() => ChangeData(clicker, trackObject)); //change elements in playmode
        }
    }

    public void LightTrackItems()
    {
        foreach (var trackObject in trackObjects)
        {
            Image trackImage = trackObject.GetComponentInChildren<Image>();

            if (trackObject.UniqueCompleted) //then Light up
            {
                trackImage.color = new Color(1,1,1,1);
            }
            else //darken
            {
                trackImage.color = new Color32(145, 145, 145, 255);
            }
        }
    }

    
    /// <summary>
    /// bind to the track button
    /// </summary>
    public void SetTrack(int chosenTrackID)
    {
        for (int i = 0; i <= trackObjects.Count; i++)
        {
            if (chosenTrackID == i)
            {
                CurrentTrack = trackObjects[i];
                return;
            }
        }

        Debug.LogError("Error finding a track");
    }

    /// <summary>
    /// change elements in playmode
    /// </summary>
    private void ChangeData(Clicker clicker, GameObject trackObject)
    {
        CurrentTrackChanged = true;

        //get pic from tracklist item
        Transform picTransform = trackObject.transform.Find("Pic");
        Image pic = picTransform.GetComponent<Image>();
        clicker.Pic = pic.sprite; //finally set pic
        clicker.ClickerPic.GetComponent<Image>().sprite = clicker.Pic;
        //set current track to source clip
        AudioSource source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        source.clip = CurrentTrack.Clip;

    }
}
