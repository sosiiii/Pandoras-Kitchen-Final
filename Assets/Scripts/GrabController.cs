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

        /*RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == "Holdable")
        {
            //grab object
            if (Input.GetKeyDown(KeyCode.G) && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }

            //release object
            else if (Input.GetKeyDown(KeyCode.G))
            {
                if (GetComponent<Player>().playerIsFlipped == false)
                {
                    grabbedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * throwForce;
                }

                else
                {
                    grabbedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * throwForce;
                }

                grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }*/
        if (hit.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.G) && grabbedObject == null)
            {
                grabbedObject = hit[0].collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }

        else if (hit.Count == 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (GetComponent<Player>().playerIsFlipped == false)
                {
                    grabbedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * throwForce;
                }

                else
                {
                    grabbedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * throwForce;
                }

                grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
}
