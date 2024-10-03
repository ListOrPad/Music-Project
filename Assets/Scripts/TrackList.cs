using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackList : MonoBehaviour
{
    public List<AudioClip> AudioTracks;
    public bool TrackChanged { get; set; }

    private void ChangeTrack()
    {
        TrackChanged = true;
    }

}
