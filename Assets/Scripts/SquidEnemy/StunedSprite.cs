using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunedSprite : MonoBehaviour
{
    public GameObject enemy;
    Transform myTransform;
    public float StunTime = 10;
    public bool carried;


    private void Start()
    {
        StartCoroutine(Stunned());
    }

    private void Update()
    {
        if (carried)
        {
            StopCoroutine(Stunned());
        }
        else if (!carried)
        {
            StartCoroutine(Stunned());
        }
    }

    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(StunTime);
        myTransform = transform;
        Instantiate(enemy, myTransform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
