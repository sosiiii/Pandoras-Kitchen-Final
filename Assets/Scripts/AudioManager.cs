using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> clips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound(0);
    }

    public void PlaySound(int ID)
    {
        audioSource.clip = clips[ID];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
