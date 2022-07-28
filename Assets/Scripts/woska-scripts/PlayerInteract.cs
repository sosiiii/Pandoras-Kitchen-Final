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
                    
                itemInHand = currentObject;
                itemInHand = pickable.PickUp();
                itemInHand.transform.parent = itemSlot;
                itemInHand.transform.localPosition = Vector3.zero;
            }
            else if (currentObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact(this);
            }
  
        }
        public void ThrowItem(InputAction.CallbackContext context)
        {
            if(!context.started) return;

            if (itemInHand != null)
            {
                itemInHand.GetComponent<PickableItem>().Throw();
                itemInHand = null;
            }

        }

        private void DropItemInHand()
        {
            if (itemInHand != null)
            {
                itemInHand.GetComponent<IPickable>().Drop();
                itemInHand = null;
            }
        }
    }
}