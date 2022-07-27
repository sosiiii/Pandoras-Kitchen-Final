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
    public float putDistance;
    public float throwForce;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        rb.Cast(transform.right, filter, hit, rayDistance);

        /*
        if (grabbedObject == null)
        {
            if (hit.Count > 0 && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.G)))
            {
                grabbedObject = hit[0].collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                grabbedObject.transform.SetParent(null);
                grabbedObject.transform.position = transform.position + transform.right * putDistance;
                grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
                grabbedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                grabbedObject = null;
            }

            else if (Input.GetKeyDown(KeyCode.G))
            {
                grabbedObject.transform.SetParent(null);
                grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
                grabbedObject.transform.position = transform.position + transform.right * putDistance;
                grabbedObject.GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
                grabbedObject = null;
            }
        }*/
    }
}
