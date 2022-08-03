using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettings : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;

    private void Awake()
    {
        Unpause();
        pauseCanvas.gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
    }
}
