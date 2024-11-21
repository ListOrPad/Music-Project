using UnityEngine;
using UnityEngine.UI;

public class VoteSystem : MonoBehaviour
{
    [SerializeField] private GameObject voter;
    [SerializeField] private TrackList trackList;

    [Header("UI elements")]
    [SerializeField] private Button voteUpBtn;
    [SerializeField] private Button voteDownBtn;

    public bool IsVoted { get; set; }

    private void Start()
    {
        voteUpBtn.onClick.AddListener(() => Vote(true));
        voteDownBtn.onClick.AddListener(() => Vote(false));
    }

    public void Appear()
    {
        voter.SetActive(true);
    }

    private void Vote(bool up)
    {
        voter.SetActive(false);

        if (up)
        {
            trackList.CurrentTrack.VoteUp = true;
        }
        else
        {
            trackList.CurrentTrack.VoteUp = false;
        }

        IsVoted = true;
    }
}
