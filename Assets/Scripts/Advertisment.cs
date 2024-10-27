using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Advertisment : MonoBehaviour
{
    [field:SerializeField] public GameObject AdLock { get; private set; }

    public int CurrentAdID { get; private set; }

    public bool IsWatched { get; set; }

    // Subscribe to the ad opening event in OnEnable
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // Unsubscribe from the ad opening event in OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // Method subscribed to receive a reward
    private void Rewarded(int id)
    {
        if (id == 1)
        {
            UnlockBookmarks();
        }
        if (id >= 2 && id <= 7)
        {
            IsWatched = true;
            CurrentAdID = id - 2;
        }
    }

    // Method for calling video ads
    public void ExampleOpenRewardAd(int id)
    {
        // Call the method to open video ads
        YandexGame.RewVideoShow(id);
    }

    private void UnlockBookmarks()
    {
        AdLock.SetActive(false);
    }
}
