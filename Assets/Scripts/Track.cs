using UnityEngine;

public class Track : MonoBehaviour
{
    public bool UniqueCompleted { get; set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }

    [SerializeField] private GameObject[] vote;

    public bool VoteUp { get; set; }

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
    }
}
