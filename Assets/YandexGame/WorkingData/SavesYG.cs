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

        public bool IsSpin;

        public bool[] IsPinPurchased = new bool[9];
        public bool[] IsBackgroundPurchased = new bool[9];
        public bool[] IsDevicePurchased = new bool[6];

        public SavesYG()
        {
            Level = 1;
            IsPinPurchased[0] = true;
            IsBackgroundPurchased[0] = true;
            IsDevicePurchased[0] = true;
        }
    }
}