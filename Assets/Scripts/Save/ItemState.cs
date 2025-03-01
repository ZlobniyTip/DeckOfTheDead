using System;
using Card;

namespace Save
{
    [Serializable]
    public class ItemState
    {
        public ItemStatus Status { get; set; }

        public CardStatus SelectedStatus { get; set; }

        public int Level { get; set; }

        public event Action Changed;

        public void SetStatus(ItemStatus status)
        {
            Status = status;
            Changed?.Invoke();
        }

        public void SetSelectedStatus(CardStatus status)
        {
            SelectedStatus = status;
        }

        public void SetParameters(int level)
        {
            Level = level;
        }

        public ItemState(ItemStatus status)
        {
            Status = status;
        }
    }
}