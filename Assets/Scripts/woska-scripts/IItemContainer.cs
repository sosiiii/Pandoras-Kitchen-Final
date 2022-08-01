using UnityEngine;

namespace woska_scripts
{
    public interface IItemContainer
    {
        bool ContainsItem(Item item);
        Item RemoveItem();
        bool AddItem(Item item);
        bool IsFull();

        Vector3 GetPosition();

        void SetLocalPosition(Vector3 position);
    }
}