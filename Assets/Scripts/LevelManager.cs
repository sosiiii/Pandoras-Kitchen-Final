using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    private Timer timer;

    [Header("Canvases")]
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas scoreCanvas;

    [SerializeField] private Transform respawnPoint;

    [SerializeField] private GameObject wasdaPlayer;
    [SerializeField] private GameObject arrowsPlayer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void OnEnable()
    {
        Player.playerDeath += PlayerDeath;
    }

    private void PlayerDeath(GameObject obj, bool wasda)
    {
        StartCoroutine(RespawnPlayer(obj, wasda));
    }

    IEnumerator RespawnPlayer(GameObject player, bool wasda)
    {
        Destroy(player);
        yield return new WaitForSeconds(1f);
        if(wasda)
            Instantiate(wasdaPlayer, respawnPoint.position, Quaternion.identity);
        else
            Instantiate(arrowsPlayer, respawnPoint.position, Quaternion.identity);
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
