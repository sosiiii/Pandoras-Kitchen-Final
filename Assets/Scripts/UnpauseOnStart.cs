using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnpauseOnStart : MonoBehaviour
{
    void Awake()
    {
        Unpause();
    }

    public void Unpause()
    {
        Time.timeScale = 0.92f;
        AudioListener.pause = false;

        //Disabling Player Controls
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        if (playerInputs == null) return;

        foreach (var playerInput in playerInputs)
        {
            playerInput.enabled = true;
        }
    }
}
