using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DoomMusicEasterEgg : MonoBehaviour
{
    [SerializeField] AudioClip doomSound;
    [SerializeField] bool shouldWork;
    [SerializeField] Image RED;
    [SerializeField] float timer;
    float startTimer;
    bool REEED;
    AudioSource myAudio;

    private void Start()
    {
        startTimer = timer;
        myAudio = GetComponent<AudioSource>();
    }
    public void EasterEgg(InputAction.CallbackContext context)
    {
        if (context.performed && shouldWork)
        {
            timer = startTimer;
            REEED = true;
            myAudio.clip = doomSound;
            myAudio.Play();
        }
    }

    private void Update()
    {
        if (REEED && shouldWork)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            RED.gameObject.SetActive(true);
            RED.color = new Color(255, 0, 0, Random.Range(0, 0.5f));
        }

    }
}
