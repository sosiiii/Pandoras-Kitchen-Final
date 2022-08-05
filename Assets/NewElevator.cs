using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewElevator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private List<Transform> floors;
    [SerializeField] private Transform cabin;

    [SerializeField] private float speed;
    [SerializeField] private float waitTime;

    private int currentFloorIndex = 0;

    private bool isGoingDown;

    private void OnValidate()
    {        
        cabin.transform.position = floors[currentFloorIndex].transform.position;
    }

    void Start()
    {
        cabin.transform.position = floors[currentFloorIndex].transform.position;

        StartCoroutine(ElevatorLogic());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ElevatorLogic()
    {

        while (true)
        {

            yield return new WaitForSeconds(waitTime);

            if (currentFloorIndex == 0)
                isGoingDown = true;
            else if (currentFloorIndex == floors.Count - 1)
                isGoingDown = false;
            
            
            var nextFloor = isGoingDown ? currentFloorIndex + 1 : currentFloorIndex - 1;

            yield return StartCoroutine(ElevatorChangeFloor(floors[currentFloorIndex], floors[nextFloor]));

            currentFloorIndex = nextFloor;
        }
    }

    IEnumerator ElevatorChangeFloor(Transform current, Transform next)
    {
        var start = current.position;
        var end = next.position;
        float t = 0;
        while(t < 1)
        {
            cabin.position = Vector3.Lerp(start,end,t);
            t = t + Time.deltaTime / speed;
            yield return new WaitForEndOfFrame();
        }
        cabin.position = end;
        yield return null;
    }
}
