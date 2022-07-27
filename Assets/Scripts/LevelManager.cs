using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        LevelGameOver();
    }

    private void LevelGameOver()
    {
        if (timer.timeValue <= 0)
        {
            // ak je �as men�� ako 0 tak hra skon�� teraz sa len na��ta znovu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
