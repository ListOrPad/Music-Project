using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class AudioTrack : MonoBehaviour
{
    private AudioClip currentTrack;
    private TrackList tracks;
    public void PlayTrack()
    {
         SoundManager.Instance.PlaySound(currentTrack);
    }

    private void PauseTrack()
    {

    }

    void Update()
    {

        //    if (tracks.TrackChanged)
        //    {
        //        currentTrack = GetNewTrack();
        //    }
    }

    

    private AudioClip GetNewTrack()
    {
        return tracks.AudioTracks[0];
    }

}
