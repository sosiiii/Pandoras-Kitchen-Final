using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerInteraction : MonoBehaviour
{
    [field: SerializeField] public float InteractionDistance { get; private set; } = 1f;

    public Interactable ClosestInteractable { get; private set; } = null;
    private CircleCollider2D circleCollider2D;


    HashSet<Interactable> ClosestInteractables = new HashSet<Interactable>();

    private void OnValidate()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();


        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = InteractionDistance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        ClosestInteractable?.DeActivate();
        ClosestInteractable = FindClosest();

        if (ClosestInteractable == null) return;
        ClosestInteractable.Activate();

        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Pushed");
            ClosestInteractable.Interaction();
        }
    }
    private Interactable FindClosest()
    {
        float currentMinDistance = float.MaxValue;
        Interactable currentInteractable = null;
        foreach (Interactable interactable in ClosestInteractables)
        {
            float distance = Vector2.Distance(transform.position, interactable.transform.position);

            if(distance <= currentMinDistance)
            {
                currentMinDistance = distance;
                currentInteractable = interactable;
            }
        }
        return currentInteractable;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Interactable>(out Interactable currentInteractable);

        if (currentInteractable == null) return;
        ClosestInteractables.Add(currentInteractable);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.TryGetComponent<Interactable>(out Interactable currentInteractable);
        if (currentInteractable == null) return;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.TryGetComponent<Interactable>(out Interactable currentInteractable);
        if (currentInteractable == null) return;
        ClosestInteractables.Remove(currentInteractable);
    }

    private void OnDrawGizmosSelected()
    {
       foreach(Interactable interactable in ClosestInteractables)
       {
            Gizmos.color = interactable == ClosestInteractable ?  Color.green : Color.black;

            Gizmos.DrawLine(transform.position, interactable.transform.position);
       }
    }
}
