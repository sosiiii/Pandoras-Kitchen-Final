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
    [SerializeField] Image Zeus;
    [SerializeField] GameObject first;
    [SerializeField] GameObject second;
    [SerializeField] Sprite NewZeusSprite;
    float startTimer;
    bool REEED;
    AudioSource myAudio;
    bool started = true;

    private void Start()
    {

        startTimer = timer;
        
        myAudio = GetComponent<AudioSource>();
    }
    public void EasterEgg(InputAction.CallbackContext context)
    {
        if (context.performed && shouldWork && started == true)
        {
            timer = startTimer;
            REEED = true;
            myAudio.clip = doomSound;

            myAudio.Play();
            started = false;
            first.SetActive(true);
        }
    }

    private void Update()
    {
        if (REEED && shouldWork)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && RED)
        {
            Zeus.sprite = NewZeusSprite;

            RED.gameObject.SetActive(true);
            RED.color = new Color(255, 0, 0, Random.Range(0, 0.5f));

            Time.timeScale = 1.5f;

            second.SetActive(false);
        }

        if (timer <= startTimer/2 && timer > 0)
        {
            first.SetActive(false);
            second.SetActive(true);
        }

    }
}
