using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSystem : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //unlock next scene
            PlayerPrefs.SetInt(nextSceneName, 1);
            SceneManager.LoadScene("LevelSelection");
        }
    }
}
