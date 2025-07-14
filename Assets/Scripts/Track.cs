using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Track : MonoBehaviour
{
    [field: SerializeField] public AudioClip Clip { get; private set; }

    public bool UniqueCompleted { get; set; }
    public bool[] starsOpenedEarlier = new bool[3] { false, false, false };

    //votes
    [SerializeField] private GameObject[] vote;
    public bool VoteUp { get; set; }
    public bool VoteChanged { get; set; }

    public void ActivateVote()
    {
        if (VoteUp)
        {
            vote[0].SetActive(true);
            vote[1].SetActive(false);
        }
        else
        {
            vote[1].SetActive(true);
            vote[0].SetActive(false);
        }

        VoteChanged = true;
    }
}
