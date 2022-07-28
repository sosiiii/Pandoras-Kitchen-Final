using System;
using Unity.VisualScripting;
using UnityEngine;

namespace woska_scripts
{
    public class PlayerObjectDetector : MonoBehaviour
    {
        [SerializeField] private float interactionRadius;
        [SerializeField] private LayerMask interactableLayerMask;
        [SerializeField] private Transform interactionPoint;
        
        private readonly Collider2D[] collider = new Collider2D[1];


        public GameObject currentObject => collider[0] != null ? collider[0].gameObject : null;
        
        

        private void Update()
        {
            if (currentObject != null)
            {
                currentObject.GetComponent<Highlight>().ToggleHighlight(false);
            }

            if (Physics2D.OverlapCircleNonAlloc(interactionPoint.position, interactionRadius, collider,
                    interactableLayerMask) == 0)
            {
                collider[0] = null;
                return;
            }
            currentObject.GetComponent<Highlight>().ToggleHighlight(true);
        }
    }
}