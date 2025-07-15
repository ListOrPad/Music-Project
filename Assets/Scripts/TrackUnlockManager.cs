using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackUnlockManager : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] private Score score;
    [SerializeField] private Advertisment advertisment;
    [SerializeField] private ProgressBar progressBar;

    [Header("Tracks")]
    [SerializeField] private Track halfUniquesTrack;
    [SerializeField] private Track maxUniquesTrack;
    [SerializeField] private Track thirtyfiveStarsTrack;
    [SerializeField] private Track fiftyStarsTrack;
    [SerializeField] private List<Track> starOpenedTracks = new List<Track>(quantityOfOpenableTracks); //all opened for stars tracks except for two traqcks that are 35 and 50 stars cost
    [SerializeField] private List<Track> rewAdTracks = new List<Track>(6);

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI halfUniquesText;
    [SerializeField] private TextMeshProUGUI maxUniquesText;
    [SerializeField] private TextMeshProUGUI thirtyfiveStarsText;
    [SerializeField] private TextMeshProUGUI fiftyStarsText;
    [SerializeField] private List<TextMeshProUGUI> starPriceText = new List<TextMeshProUGUI>(17);

    [Header("Images")]
    [SerializeField] private Sprite rewAdSprite;

    private const int firstPrice = 3;
    private const int quantityOfOpenableTracks = 17;
    private const int finalPrice = firstPrice + quantityOfOpenableTracks;

    private void Start()
    {
        progressBar.ThresholdReached += Unlock;
        progressBar.ThresholdReached += UpdateUnlockText;

        foreach (var rewAdTrack in rewAdTracks)
        {
            rewAdTrack.gameObject.GetComponent<Button>().interactable = false;
            rewAdTrack.transform.Find("Ad Pic").GetComponent<Image>().sprite = rewAdSprite;
        }

        halfUniquesTrack.gameObject.GetComponent<Button>().interactable = false;
        maxUniquesTrack.gameObject.GetComponent<Button>().interactable = false;
        thirtyfiveStarsTrack.gameObject.GetComponent<Button>().interactable = false;
        fiftyStarsTrack.gameObject.GetComponent<Button>().interactable = false;
        foreach (var track in starOpenedTracks)
        {
            track.gameObject.GetComponent<Button>().interactable = false;
        }

        thirtyfiveStarsText.text = $"35 <sprite=0>";
        fiftyStarsText.text = $"50 <sprite=0>";
        for (int i = 0; i < starOpenedTracks.Count; i++)
        {
            int starsCost = i + firstPrice;
            starPriceText[i].text = $"{starsCost} <sprite=0>";
        }

        UpdateUnlockText();
        Unlock();
    }

    private void Update()
    {
        if (TrackList.TrackFinished)
        {
            UpdateUnlockText();
            Unlock();
        }
        if(advertisment.IsWatched)
        {
            Unlock();
        }
    }

    private void UpdateUnlockText()
    {
        halfUniquesText.text = $"{score.UniqueCount} / 10";
        maxUniquesText.text = $"{score.UniqueCount} / 20";
    }

    

    private void Unlock()
    {
        //if Ad was watched earlier
        if (advertisment.IsWatched)
        {
            int chosenAdIndex = advertisment.CurrentAdID;

            for (int i = 0; i < rewAdTracks.Count; i++)
            {
                if (chosenAdIndex == i)
                {
                    rewAdTracks[chosenAdIndex].gameObject.GetComponent<Button>().interactable = true;
                    rewAdTracks[chosenAdIndex].transform.Find("Ad Pic").gameObject.SetActive(false);
                }
            }
            advertisment.IsWatched = false;
        }

        //unlock tracks opened before
        for (int i = 0; i < rewAdTracks.Count; i++)
        {
            bool wasAdViewedEarlier = MySaver.Instance.adsViewed[i];
            if (wasAdViewedEarlier)
            {
                rewAdTracks[i].gameObject.GetComponent<Button>().interactable = true;
                rewAdTracks[i].transform.Find("Ad Pic").gameObject.SetActive(false);
            }
        }

        if (score.UniqueCount >= 10)
        {
            halfUniquesTrack.gameObject.GetComponent<Button>().interactable = true;
            halfUniquesText.gameObject.SetActive(false);
        }
        if (score.UniqueCount >= 20)
        {
            maxUniquesTrack.gameObject.GetComponent<Button>().interactable = true;
            maxUniquesText.gameObject.SetActive(false);
        }
        if (score.ScoreCount >= 35)
        {
            thirtyfiveStarsTrack.gameObject.GetComponent<Button>().interactable = true;
            thirtyfiveStarsText.gameObject.SetActive(false);
        }
        if (score.ScoreCount >= 50)
        {
            fiftyStarsTrack.gameObject.GetComponent<Button>().interactable = true;
            fiftyStarsText.gameObject.SetActive(false);
        }

        int index = 0;
        while (score.ScoreCount >= firstPrice && index < quantityOfOpenableTracks)
        {
            //if actual score is higher/equal to the price of an item then we open it
            if (score.ScoreCount >= firstPrice + index) 
            {
                starOpenedTracks[index].gameObject.GetComponent<Button>().interactable = true;
                starPriceText[index].gameObject.SetActive(false);
            }

            index++;
            
        }
    }
}
