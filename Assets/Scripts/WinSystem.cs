using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSystem : MonoBehaviour
{
    [Header("Unlock Scene")]
    public string nextSceneName;

    [SerializeField] private int scoreToUnlockNextLevel;

    Score score;

    private void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    private void Update()
    {
        if (score.score >= scoreToUnlockNextLevel)
        {
            //Unlock next level
            PlayerPrefs.SetInt(nextSceneName, 1);
        }
    }
}
