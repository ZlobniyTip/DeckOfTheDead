using System;

[Serializable]
public class ItemState
{
    public event Action Changed;

    public ItemStatus Status;
    public CardStatus SelectedStatus;
    public int Level;

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