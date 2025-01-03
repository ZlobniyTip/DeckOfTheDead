using System;

[Serializable]
public class ItemState
{
    public event Action Changed;

    public ItemStatus Status;
    public int Level;

    public void SetStatus(ItemStatus status)
    {
        Status = status;
        Changed?.Invoke();
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