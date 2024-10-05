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

        public int Pin;
        public int Device;
        public int Background;
        public int Stage;
        public int Money;
        public int Level;

        public SavesYG()
        {
            Level = 1;
        }
    }
}