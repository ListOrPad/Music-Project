using UnityEngine;
using UnityEngine.UI;
using YG;

public class Localization : MonoBehaviour
{
    [SerializeField] private Button ruLanguageButton;
    [SerializeField] private Button enLanguageButton;
    //serialize buttons for spanish, japanese, german and turkish 
    //or maybe make Button[] array
    public string CurrentLang { get; private set; }
    public bool LangChanged { get; set; }

    private void Start()
    {
        CurrentLang = YandexGame.lang;
        UpdateLanguageButton();
    }

    private void UpdateLanguageButton()
    {
        if (CurrentLang == "en")
        {
            enLanguageButton.gameObject.SetActive(true);
            ruLanguageButton.gameObject.SetActive(false);
        }
        else
        {
            ruLanguageButton.gameObject.SetActive(true);
            enLanguageButton.gameObject.SetActive(false);
        }
    }

    //on language button click
    public void ChangeLanguageButton()
    {
        if (CurrentLang != YandexGame.lang)
        {
            CurrentLang = YandexGame.lang;
            UpdateLanguageButton();
            LangChanged = true;
        }
    }
}
