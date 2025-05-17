using UnityEngine;
using UnityEngine.UI;

public class VoteSystem : MonoBehaviour
{
    [SerializeField] private GameObject voter;

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
            TrackList.CurrentTrack.VoteUp = true;
        }
        else
        {
            TrackList.CurrentTrack.VoteUp = false;
        }

        IsVoted = true;
    }
}
