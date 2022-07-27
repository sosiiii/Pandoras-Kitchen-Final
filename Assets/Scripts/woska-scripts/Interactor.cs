using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private Transform interactionPoint;

    private readonly Collider2D[] colliders = new Collider2D[3];
    private int numFound;
    private IInteractable currentInteractable;
    private InteractableOutline currentOutline;

    // Update is called once per frame
    void Update()
    {
        numFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableLayerMask);

        if (numFound == 0)
        {
            DeactivateOld();
            return;
        }

        colliders[0].TryGetComponent<IInteractable>(out IInteractable interactable);

        if (interactable == null) throw new System.Exception("No IInteractable found!");

        currentInteractable = interactable;

        colliders[0].TryGetComponent<InteractableOutline>(out InteractableOutline interactableOutline);


        if(currentOutline != interactableOutline)
        {
            if (currentOutline != null)
                currentOutline.SetActive(false);
        }
        currentOutline = interactableOutline;

        if(currentOutline != null)
        {
            if(!currentOutline.IsDisplayed)
                currentOutline.SetActive(true);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            currentInteractable.Interact(this);
        }
            
    }

    private void DeactivateOld()
    {
        if (currentInteractable != null)
            currentInteractable = null;

        if (currentOutline != null)
        {
            currentOutline.SetActive(false);
            currentOutline = null;
        }

    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
