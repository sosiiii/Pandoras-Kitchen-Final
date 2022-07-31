using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace woska_scripts
{
    public class PlayerInteract : MonoBehaviour
    {
        private PlayerObjectDetector _playerObjectDetector;


        [SerializeField] private Transform itemSlot;

        private GameObject itemInHand;
        
        public IPickable currentItemInHand { get; private set; }

        public bool HasItem => currentItemInHand != null;
        private void Awake()
        {
            _playerObjectDetector = GetComponent<PlayerObjectDetector>();
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
                DropItemInHand();
                    
                PickUpItem(pickable);
            }
            else if (currentObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact(this);
            }
  
        }

        public void ClearItemSlot()
        {
            itemInHand = null;
            currentItemInHand = null;
        }

        public void PickUpItem(IPickable pickable)
        {
            itemInHand = pickable.GetOwner();
            currentItemInHand = pickable;
            currentItemInHand.PickUp();
            currentItemInHand.ChangeParent(itemSlot);
        }
        public void ThrowItem(InputAction.CallbackContext context)
        {
            if(!context.started) return;

            if (itemInHand != null)
            {
                itemInHand.GetComponent<PickableItem>().Throw();
                ClearItemSlot();
            }

        }

        private void DropItemInHand()
        {
            if (itemInHand != null)
            {
                itemInHand.GetComponent<IPickable>().Drop();
                ClearItemSlot();
            }
        }
    }
}