using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackUnlockManager : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] private Score score;
    [SerializeField] private Advertisment advertisment;
    private TrackList trackList;

    [Header("Tracks")]
    [SerializeField] private Track halfUniquesTrack;
    [SerializeField] private Track maxUniquesTrack;
    [SerializeField] private Track thirtyfiveStarsTrack;
    [SerializeField] private Track fiftyStarsTrack;
    [SerializeField] private List<Track> rewAdTracks = new List<Track>(6);

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI halfUniquesText;
    [SerializeField] private TextMeshProUGUI maxUniquesText;
    [SerializeField] private TextMeshProUGUI thirtyfiveStarsText;
    [SerializeField] private TextMeshProUGUI fiftyStarsText;

    [Header("Images")]
    [SerializeField] private Sprite rewAdSprite;

    private void Start()
    {
        foreach (var rewAdTrack in rewAdTracks)
        {
            rewAdTrack.gameObject.GetComponent<Button>().interactable = false;
            rewAdTrack.transform.Find("Ad Pic").GetComponent<Image>().sprite = rewAdSprite;
        }

        halfUniquesTrack.gameObject.GetComponent<Button>().interactable = false;
        maxUniquesTrack.gameObject.GetComponent<Button>().interactable = false;
        thirtyfiveStarsTrack.gameObject.GetComponent<Button>().interactable = false;
        fiftyStarsTrack.gameObject.GetComponent<Button>().interactable = false;

        thirtyfiveStarsText.text = $"35 <sprite=0>";
        fiftyStarsText.text = $"50 <sprite=0>";

        UpdateText();
        Unlock();
    }

    private void Update()
    {
        if (TrackList.TrackFinished)
        {
            UpdateText();
            Unlock();
        }
        if(advertisment.IsWatched)
        {
            Unlock();
        }
    }

    private void UpdateText()
    {
        halfUniquesText.text = $"{score.UniqueCount} / 15";
        maxUniquesText.text = $"{score.UniqueCount} / 30";
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
                    rewAdTracks[chosenAdIndex].transform.Find("Ad Pic").gameObject.SetActive(false); //will this implementation be saved through sessions????????????????????????
                }
            }
            advertisment.IsWatched = false;
        }

        if (score.UniqueCount >= 15)
        {
            halfUniquesTrack.gameObject.GetComponent<Button>().interactable = true;
            halfUniquesText.gameObject.SetActive(false);
        }
        if (score.UniqueCount >= 30)
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
    }
}
