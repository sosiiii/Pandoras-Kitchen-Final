using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float Timer;
    [SerializeField] private float StarterTimer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");
            //NoMovement();
            collision.GetComponent<Player>().Kill();
        }
    }

    IEnumerator NoMovement()
    {
        GetComponent<SquidScript>().speed = 0;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SquidScript>().speed = 2;
    }
}
