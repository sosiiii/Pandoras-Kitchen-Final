using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoomMusicEasterEgg : MonoBehaviour
{
    [SerializeField] AudioClip doomSound;
    [SerializeField] bool shouldWork = true;


    public void EasterEgg(InputAction.CallbackContext context)
    {
        if (context.performed && shouldWork)
        {
            GetComponent<AudioSource>().clip = doomSound;
            GetComponent<AudioSource>().Play();
        }
    }
}
