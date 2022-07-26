using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private ContactFilter2D filter;
    private GameObject grabbedObject;
    public float throwForce;

    void Update()
    {
        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        GetComponent<Rigidbody2D>().Cast(transform.right, filter, hit, rayDistance);

        if (hit.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.G) && grabbedObject == null)
            {
                grabbedObject = hit[0].collider.gameObject;
                grabbedObject.GetComponent<PolygonCollider2D>().enabled = false;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }

        else if (hit.Count == 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Player dropped game object ");
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.GetComponent<PolygonCollider2D>().enabled = true;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
}
