using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicPlayer : MonoBehaviour
{
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            GetComponent<AudioSource>().Pause();
        }

        else
        {
            GetComponent<AudioSource>().UnPause();
        }
    }
}
