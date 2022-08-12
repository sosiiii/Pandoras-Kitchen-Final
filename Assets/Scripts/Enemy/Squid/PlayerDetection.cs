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
            NoMovement();
            collision.GetComponent<Player>().HP = 0;
        }
    }

    IEnumerator NoMovement()
    {
        GetComponent<SquidScript>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SquidScript>().enabled = true;
    }
}
