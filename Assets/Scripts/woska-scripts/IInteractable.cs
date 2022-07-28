using UnityEngine;

namespace woska_scripts
{
    public interface IInteractable
    {
        public bool Interact(PlayerInteract playerInteract);
    }
}