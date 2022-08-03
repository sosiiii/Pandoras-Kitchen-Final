using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(DestroyThisGameObject());
    }

    private IEnumerator DestroyThisGameObject()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}