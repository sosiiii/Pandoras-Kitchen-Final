using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Timer timer;

    [Header("Canvases")]
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas scoreCanvas;

    [SerializeField] private Transform respawnPoint;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void OnEnable()
    {
        Player.playerDeath += PlayerDeath;
    }

    private void PlayerDeath(GameObject obj)
    {
        obj.transform.position = respawnPoint.position;
    }

    private void OnDisable()
    {
        Player.playerDeath -= PlayerDeath;
    }

    void Update()
    {
        LevelGameOver();
    }

    private void LevelGameOver()
    {
        if (timer.timeValue <= 0)
        {
            // when is time less then 0 then show score canvas
            gameCanvas.gameObject.SetActive(false);
            scoreCanvas.gameObject.SetActive(true);
        }
    }
}
