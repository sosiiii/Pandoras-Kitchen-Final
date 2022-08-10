using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayedScene : MonoBehaviour
{
    public int sceneIndex;
    public int lastPlayedSceneStatus;

    private void Awake()
    {
        PlayerPrefs.SetInt("LastPlayedScene", sceneIndex);
    }
}
