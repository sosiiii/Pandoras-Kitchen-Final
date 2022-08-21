using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    private static MenuMusicPlayer _musicPlayer;

    private void Awake()
    {
        if(_musicPlayer != null) Destroy(gameObject);
        else
        {
            _musicPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 6)
        {
            audioSource.Pause();
        }

        else
        {
            audioSource.UnPause();
        }
    }
}
