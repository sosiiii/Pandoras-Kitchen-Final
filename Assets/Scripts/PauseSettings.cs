using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PauseSettings : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private float timeScale = 0.8f;
    private bool isGamePaused;

    private void Awake()
    {
        isGamePaused = false;
        Unpause();
        pauseCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        Time.timeScale = timeScale;
        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;

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

    public void Unpause()
    {
        Time.timeScale = timeScale;
        Debug.Log(Time.timeScale);
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
