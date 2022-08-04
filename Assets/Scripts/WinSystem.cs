using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSystem : MonoBehaviour
{
    [Header("Unlock Scene")]
    public string nextSceneName;

    public void LevelIsWon()
    {
        PlayerPrefs.SetInt(nextSceneName, 1);
    }
}
