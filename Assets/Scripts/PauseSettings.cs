using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PauseSettings : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private float timeScale = 0.92f;
    public bool isGamePaused;

    LevelManager levelManager;

    private void Awake()
    {
        isGamePaused = false;
        Unpause();
        pauseCanvas.gameObject.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;

        if (levelManager.gameEnded == false)
        {
            if (isGamePaused)
            {
                Unpause();
                pauseCanvas.gameObject.SetActive(false);
            }

            else
            {
                Pause();
                pauseCanvas.gameObject.SetActive(true);
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        AudioListener.pause = true;

        //Disabling Player Controls
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        foreach (var playerInput in playerInputs)
        {
            playerInput.enabled = false;
        }
    }

    public void PauseOnWin()
    {
        Time.timeScale = 0f;
        isGamePaused = true;

        //Disabling Player Controls
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        foreach (var playerInput in playerInputs)
        {
            playerInput.enabled = false;
        }
    }

    public void Unpause()
    {
        Time.timeScale = timeScale;
        isGamePaused = false;
        AudioListener.pause = false;

        //Disabling Player Controls
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        foreach (var playerInput in playerInputs)
        {
            playerInput.enabled = true;
        }
    }
}
