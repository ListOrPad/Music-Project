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
    }
}
