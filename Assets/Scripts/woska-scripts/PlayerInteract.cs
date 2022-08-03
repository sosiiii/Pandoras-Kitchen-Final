using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace woska_scripts
{
    public class PlayerInteract : MonoBehaviour
    {
        private PlayerObjectDetector _playerObjectDetector;
        private PlayerCombat _playerCombat;
        public IPickable currentItemInHand { get; private set; }

        public IItemContainer ItemSlot { get; private set; }
        public bool HasItem => currentItemInHand != null;

        [SerializeField] private SolidItem solidItemPrefab;
        [SerializeField] private ItemSlot _itemSlotGO;
        private void Awake()
        {
            _playerObjectDetector = GetComponent<PlayerObjectDetector>();
            _playerCombat = GetComponent<PlayerCombat>();
            ItemSlot = _itemSlotGO;
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if(!context.started) return;
            
            var currentObject = _playerObjectDetector.currentObject;
            if (currentObject == null)
            {
                DropItemInHand();
            }
            else if (currentObject.TryGetComponent<IPickable>(out IPickable pickable))
            {
                if(ItemSlot.IsFull()) DropItemInHand();
                PickUpItem(pickable);
            }
            else if (currentObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact(this);
            }
        }
        
        public void PickUpItem(IPickable pickable)
        {
            var itemToPickUp = pickable.GetItem();
            pickable.DestroyItem();
            ItemSlot.AddItem(itemToPickUp);
        }
        public void ThrowItem(InputAction.CallbackContext context)
        {
            if(!context.started) return;
            
            if(!ItemSlot.IsFull() && _playerCombat.enabled) return;

            var item = InitItem();
            item.Throw();
            
        }

        private void DropItemInHand()
        {
            if(!ItemSlot.IsFull()) return;

            InitItem();
        }

        private IPickable InitItem()
        {
            var itemRemoved = ItemSlot.RemoveItem();
            
            //Creates a in world item
            var instantiateItem = Instantiate(solidItemPrefab, ItemSlot.GetPosition(), transform.rotation);

            //Sets in world item to correct sprite
            instantiateItem.Init(itemRemoved);

            return instantiateItem;
        }
    }
}