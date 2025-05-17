using UnityEngine;

public class Track : MonoBehaviour
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField] public string WebGLPath { get; private set; }

    public bool UniqueCompleted { get; set; }

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
