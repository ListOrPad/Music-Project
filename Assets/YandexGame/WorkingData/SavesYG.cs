
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

        // Ваши сохранения

        public int Score = 0;
        public int UniqueCount = 0;
        public bool[] UniquesCompleted = new bool[30];
        public bool[] VotesUp = new bool[30];
        public bool[] VoteChanges = new bool[30];
        public bool[] AdsViewed = new bool[6];

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

        }
    }
}
