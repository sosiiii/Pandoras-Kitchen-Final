using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOnFloor : MonoBehaviour
{
    [SerializeField] private int _elevatorFloor;

    Elevator elevator;

    void Awake()
    {
        elevator = FindObjectOfType<Elevator>();
    }

    void Update()
    {
        if (elevator.elevatorFloorNumber == _elevatorFloor)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        else if (elevator.elevatorFloorNumber != _elevatorFloor)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
