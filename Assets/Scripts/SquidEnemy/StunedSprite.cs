using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunedSprite : MonoBehaviour
{
    public GameObject enemy;
    Transform myTransform;
    public float StunTime = 10;

    private void Start()
    {
        StartCoroutine(Stunned());
    }
    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(StunTime);
        myTransform = transform;
        Instantiate(enemy, myTransform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if ()
        {

        }
    }
}
