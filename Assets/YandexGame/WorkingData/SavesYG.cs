
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

        public int playerMoney = 0;
        public int leaderboardScore = 0;
        public int currentLevel = 1;

        public List<ItemStatus> rangeWeaponStates = new()
        {
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
            ItemStatus.NotPurchased
        };

        public List<ItemStatus> melleWeaponStates = new()
        {
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
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
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased,
            ItemStatus.NotPurchased, ItemStatus.NotPurchased
        };

        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
        }
    }
}
