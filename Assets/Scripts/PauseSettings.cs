using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PauseSettings : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    private bool isGamePaused;

    private void Awake()
    {
        isGamePaused = false;
        Unpause();
        pauseCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;

        if (isGamePaused)
        {
            Unpause();
        }

        else
        {
            Pause();
        }

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseCanvas.gameObject.SetActive(true);
        isGamePaused = true;
        AudioListener.pause = true;


        //Disabling Player Controls
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        foreach (var playerInput in playerInputs)
        {
            playerInput.enabled = false;
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseCanvas.gameObject.SetActive(false);
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
