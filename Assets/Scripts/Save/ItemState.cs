using System;

[Serializable]
public class ItemState
{
    public event Action Changed;

    public ItemStatus Status;

    public void SetStatus(ItemStatus status)
    {
        Status = status;
        Changed?.Invoke();
    }

    public ItemState(ItemStatus status)
    {
        Status = status;
    }
}