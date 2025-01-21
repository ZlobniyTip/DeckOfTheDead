using System.Collections.Generic;

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

        public float sound = 1;
        public int indexCurrentScene = 0;

        public int playerMoney = 0;
        public int leaderboardScore = 0;

        public List<ItemStatus> rangeWeaponStates = new()
        {
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased
        };


        public List<ItemStatus> melleWeaponStates = new()
        {
            ItemStatus.Purchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased
        };

        public List<ItemStatus> cardStates = new()
        {
            ItemStatus.Purchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.Purchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.Purchased,
            ItemStatus.Purchased, ItemStatus.Purchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased
        };

        public List<int> cardLevel = new()
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            
        }
    }
}
