using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public GameObject soundGameObject;

    public void PlayButtonSound()
    {
        Instantiate(soundGameObject);
    }
}
