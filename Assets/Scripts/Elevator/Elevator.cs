using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Elevator Speed")]
    public float speed;

    [Header("Floors")]
    [Range(2, 3)] public int floors;

    [Header("Floor Position")]
    public List<Vector3> floorsPos = new List<Vector3>();

    [Header("Elevator Wait Time")]
    public float howLongWaitOnFloor;

    private void Awake()
    {
        //Move elevator on start position
        transform.position = new Vector3(floorsPos[0].x, floorsPos[0].y, floorsPos[0].z);
    }

    private void Start()
    {
        StartCoroutine(ElevatorControl());
    }

    private IEnumerator ElevatorControl()
    {
        while (true)
        {
            if (floors == 2)
            {
                yield return new WaitForSeconds(howLongWaitOnFloor);
                yield return StartCoroutine(MoveElevator(floorsPos[0]));
                yield return new WaitForSeconds(howLongWaitOnFloor);
                yield return StartCoroutine(MoveElevator(floorsPos[1]));
            }

            else if (floors == 3)
            {
                yield return new WaitForSeconds(howLongWaitOnFloor);
                yield return StartCoroutine(MoveElevator(floorsPos[1]));
                yield return new WaitForSeconds(howLongWaitOnFloor);
                yield return StartCoroutine(MoveElevator(floorsPos[2]));
                yield return new WaitForSeconds(howLongWaitOnFloor);
                yield return StartCoroutine(MoveElevator(floorsPos[1]));
                yield return new WaitForSeconds(howLongWaitOnFloor);
                yield return StartCoroutine(MoveElevator(floorsPos[0]));
            }
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