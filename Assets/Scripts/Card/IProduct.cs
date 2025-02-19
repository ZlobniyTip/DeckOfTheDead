using Other;
using Save;
using UnityEngine;

namespace Card
{
    public interface IProduct
    {
        public ItemType Type { get; }
        public Sprite Icon { get; }
        public string Name { get; }
        public int Price { get; }
        public int Index { get; }
        public ItemState State { get; }

        public void Init(ItemStatus state, int level);
    }
}