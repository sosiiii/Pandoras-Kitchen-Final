using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject bullet;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        for (int i = 0; i < 8; i++)
        {
            var rot = Quaternion.AngleAxis(45 * i, Vector3.left);
            var lDirection = rot * Vector3.forward * 10 + transform.position;
  
            Instantiate(bullet, lDirection, Quaternion.identity);
        }
    }

    private void Update()
    {
        
        
    }
}
