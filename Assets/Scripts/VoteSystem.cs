using YG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoteSystem : MonoBehaviour
{
    [SerializeField] private GameObject voter;
    [SerializeField] private TrackList trackList;

    public bool VoteChanged { get; set; }

    [Header("UI elements")]
    [SerializeField] private Button voteUpBtn;
    [SerializeField] private Button voteDownBtn;
    [SerializeField] private TextMeshProUGUI voteText;

    private Localization localization;


    private void Start()
    {
        voteUpBtn.onClick.AddListener(() => Vote(true));
        voteDownBtn.onClick.AddListener(() => Vote(false));

        localization = GetComponent<Localization>();
    }

    private void Update()
    {
        if (localization.LangChanged)
        {
            TranslateText();
        }
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

        VoteChanged = true;
    }

    private void TranslateText()
    {
        if (YandexGame.lang == "ru")
            voteText.text = "Оцени Музыку";
        else if (YandexGame.lang == "en")
            voteText.text = "Rate the Music";
        else if (YandexGame.lang == "es")
            voteText.text = "Califica la Música";
        else if (YandexGame.lang == "tr")
            voteText.text = "Müziği Değerlendirin";
        else if (YandexGame.lang == "jp")
            voteText.text = "音楽を評価する";
        else if (YandexGame.lang == "de")
            voteText.text = "Bewerten Sie die Musik";

        localization.LangChanged = false;
    }
}
