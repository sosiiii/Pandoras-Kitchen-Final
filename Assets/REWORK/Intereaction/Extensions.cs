using UnityEngine;

namespace Intereaction
{
    public static class Extensions
    {
        public static bool IsInteractable(this Collider2D collider2D)
        {
            return collider2D.GetComponent<Interactable>();
        }
        public static void Interact(this Collider2D collider2D, PlayerInteraction playerInteraction)
        {
            collider2D.GetComponent<Interactable>().Interact(playerInteraction);
        }

        public static Interactable GetInteractable(this Collider2D collider2D)
        {
            return collider2D.GetComponent<Interactable>();
        }
        public static int GetPriority(this Collider2D collider2D)
        {
            return collider2D.GetComponent<Interactable>().Priority;
        }
    }
}