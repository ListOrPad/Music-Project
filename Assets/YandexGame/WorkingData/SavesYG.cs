
using System.Linq;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // My saves

        public int Score = 0;
        public int UniqueCount = 0;
        public bool[] UniquesCompleted = new bool[30];
        public bool[] VotesUp = new bool[30];
        public bool[] VoteChanges = new bool[30];
        public bool[] AdsViewed = new bool[6];
        public bool[][] StarsOpenedEarlierArray = Enumerable.Range(0, 30).Select(_ => new bool[3]).ToArray();


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

        }
    }
}
