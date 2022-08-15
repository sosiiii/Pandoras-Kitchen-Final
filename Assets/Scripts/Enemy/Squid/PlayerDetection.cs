using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float Timer;
    [SerializeField] private float StarterTimer;
    private int attackDamage;
    private void Start()
    {
        attackDamage = GetComponentInParent<SquidScript>().attackdamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");
            collision.GetComponent<Player>().Damaged(attackDamage);
        }
    }
}
