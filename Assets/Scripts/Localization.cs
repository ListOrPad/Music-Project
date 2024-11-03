using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Localization : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI voteText;
    [SerializeField] private TextMeshProUGUI autoText;
    [Space(15)]
    [SerializeField] private TMP_FontAsset jpFont;
    [SerializeField] private TMP_FontAsset regularFont;

    [SerializeField] private Button[] ChangeLangButtons;
    public string CurrentLang { get; private set; }

    private void Start()
    {
        voteText.font = regularFont;
        autoText.font = regularFont;

        CurrentLang = YandexGame.lang;

        foreach (var button in ChangeLangButtons)
        {
            button.onClick.AddListener(ChangeLang);
        }
    }

    private void TranslateText()
    {
        voteText.fontSize = 50;

        voteText.font = regularFont;
        autoText.font = regularFont;

        if (CurrentLang == "ru")
        {
            voteText.text = "Оцени Музыку";
            autoText.text = "авто";
        }
        else if (CurrentLang == "en")
        {
            voteText.text = "Rate the Music";
            autoText.text = "auto";
        }
        else if (CurrentLang == "es")
        {
            voteText.text = "Califica la Música";
            autoText.text = "auto";
        }
        else if (CurrentLang == "tr")
        {
            voteText.fontSize = 48;
            voteText.text = "Müziği Değerlendirin";
            autoText.text = "oto";
        }
        else if (CurrentLang == "jp")
        {
            voteText.font = jpFont;
            autoText.font = jpFont;
            voteText.text = "音楽を評価する";
            autoText.text = "自動";
        }
        else if (CurrentLang == "de")
        {
            voteText.text = "Bewerten Sie die Musik";
            autoText.text = "auto";
        }
    }

    public void ChangeLang()
    {
        CurrentLang = YandexGame.lang;
        TranslateText() ;
    }
}
