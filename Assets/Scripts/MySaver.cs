using System.Collections;
using UnityEngine;
using YG;

public class MySaver : MonoBehaviour
{
    public static MySaver Instance;

    private Coroutine myCoroutine;
    public int scoreCount;
    public int uniqueCount;
    public bool[] uniquesCompleted;
    public bool[] votesUp;
    public bool[] voteChanges;
    //public bool[] adsViewed;   ?????????

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    /// <summary>
    /// Get data from plugin and do with it what we want
    /// </summary>
    public void GetLoad()
    {
        scoreCount = YandexGame.savesData.Score;
        uniqueCount = YandexGame.savesData.UniqueCount;
        uniquesCompleted = YandexGame.savesData.UniquesCompleted;
        votesUp = YandexGame.savesData.VotesUp;
        voteChanges = YandexGame.savesData.VoteChanges;
    }

    /// <summary>
    /// Our method for saving
    /// </summary>
    public IEnumerator MySave()
    {
        yield return new WaitForSeconds(3);

        YandexGame.savesData.Score = scoreCount;
        YandexGame.savesData.UniqueCount = uniqueCount;
        YandexGame.savesData.UniquesCompleted = uniquesCompleted;
        YandexGame.savesData.VotesUp = votesUp;
        YandexGame.savesData.VoteChanges = voteChanges;
        //save
        YandexGame.SaveProgress();

        //reset coroutine
        myCoroutine = null;
    }

    private void Update()
    {
        if (myCoroutine != null)
        {

        }
        else
        {
            myCoroutine = StartCoroutine(MySave());
        }
    }
}