using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int elevatorFloorNumber;

    public void ElevatorFloor(int floorNumber)
    {
        elevatorFloorNumber = floorNumber;
    }
}
