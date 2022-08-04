using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<MenuMusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            audioSource.Pause();
        }

        else
        {
            audioSource.UnPause();
        }
    }
}
