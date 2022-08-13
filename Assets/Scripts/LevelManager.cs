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
        StartCoroutine(RespawnPlayer(obj));
    }

    IEnumerator RespawnPlayer(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = respawnPoint.position;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(true);
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
