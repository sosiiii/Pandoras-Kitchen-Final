using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float Timer;
    [SerializeField] private float StarterTimer;
    void Start()
    {
        Debug.Log("Hello");
        Timer = StarterTimer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Timer <= 0)
        {
            Debug.Log("Collision");
            Timer = StarterTimer;
            collision.GetComponent<Player>().HP--;
        }
    }

    void Update()
    {
        Timer -= Time.deltaTime;
    }
}
