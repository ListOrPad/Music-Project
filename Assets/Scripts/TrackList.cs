using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackList : MonoBehaviour
{
    [field: SerializeField] public List<Track> TrackObjects { get; set; }
    public static Track CurrentTrack { get; private set; }
    public static bool CurrentTrackChanged { get; set; }
    public static bool TrackFinished { get; set; }

    [SerializeField] private VoteSystem voteSystem;
    [SerializeField] private ProgressBar progressBar;

    private void Start()
    {
        //load data from MySaver
        for (int i = 0; i < TrackObjects.Count; i++)
        {
            Track track = TrackObjects[i];
            track.starsOpenedEarlier = MySaver.Instance.starsOpenedEarlierArray[i];
            track.UniqueCompleted = MySaver.Instance.uniquesCompleted[i];
            track.VoteUp = MySaver.Instance.votesUp[i];
            track.VoteChanged = MySaver.Instance.voteChanges[i];
            if (track.VoteChanged)
            {
                track.ActivateVote();
            }
        }

        LightTrackItems();
    }
    private void Update()
    {
        if (TrackFinished)
        {
            for (int i = 0; i < TrackObjects.Count; i++)
            {
                //let uniques completed be saved in MySaver
                Track track = TrackObjects[i];
                if (track.UniqueCompleted)
                {
                    MySaver.Instance.uniquesCompleted[i] = track.UniqueCompleted;
                }
            }

            LightTrackItems();
        }

        if (voteSystem.IsVoted)
        {
            for (int i = 0; i < TrackObjects.Count; i++)
            {
                //saves votes in MySaver
                Track track = TrackObjects[i];
                MySaver.Instance.votesUp[i] = track.VoteUp;
            }

            CurrentTrack.ActivateVote();

            for (int i = 0; i < TrackObjects.Count; i++)
            {
                //marks trackObjects as voted
                Track track = TrackObjects[i];
                MySaver.Instance.voteChanges[i] = track.VoteChanged;
            }

            voteSystem.IsVoted = false;
        }
    }

    public void PrepareTracklistButtons(Game game, Clicker clicker)
    {
        for (int i = 0; i < TrackObjects.Count; i++)
        {
            GameObject trackObject = TrackObjects[i].gameObject;
            Button button = trackObject.GetComponentInChildren<Button>();

            button.onClick.AddListener(game.SwitchCanvas);
            button.onClick.AddListener(() => ChangeData(clicker, trackObject)); //change elements in playmode
        }
    }

    public void LightTrackItems()
    {
        foreach (var trackObject in TrackObjects)
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
        if (chosenTrackID < 0 || chosenTrackID >= TrackObjects.Count)
        {
            Debug.LogError($"Invalid track ID: {chosenTrackID}");
            return;
        }

        var track = TrackObjects[chosenTrackID];
        CurrentTrack = track;
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

        //opened earlier stars show up
        progressBar.OpenStars();
    }
}
