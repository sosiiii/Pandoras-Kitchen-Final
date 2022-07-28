using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Elevator Speed")]
    public float speed;

    [Header("Floor Position")]
    public Vector3 numberThreeFloorPos;
    public Vector3 numberTwoFloorPos;
    public Vector3 numberOneFloorPos;

    [Header("Elevator Wait Time")]
    public float howLongWaitOnFloor;

    private void Start()
    {
        StartCoroutine(ElevatorControl());
    }

    private IEnumerator ElevatorControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(howLongWaitOnFloor);
            yield return StartCoroutine(MoveElevator(numberTwoFloorPos)); 
            yield return new WaitForSeconds(howLongWaitOnFloor);
            yield return StartCoroutine(MoveElevator(numberOneFloorPos));
            yield return new WaitForSeconds(howLongWaitOnFloor);
            yield return StartCoroutine(MoveElevator(numberTwoFloorPos));
            yield return new WaitForSeconds(howLongWaitOnFloor);
            yield return StartCoroutine(MoveElevator(numberThreeFloorPos));
        }
    }

    IEnumerator MoveElevator(Vector3 target)
    {
        float step = speed * 0.01f;
        while (Mathf.Abs(transform.position.y - target.y) > 0.05f)
        {
            MoveToFloor(step, target);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void MoveToFloor(float stepPara, Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, stepPara);
    }
}
