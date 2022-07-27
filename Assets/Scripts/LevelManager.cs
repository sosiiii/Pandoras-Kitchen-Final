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
            // ak je èas menší ako 0 tak hra skonèí teraz sa len naèíta znovu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
