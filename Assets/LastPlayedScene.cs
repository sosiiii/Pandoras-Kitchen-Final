using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayedScene : MonoBehaviour
{
    public int sceneIndex;
    public bool setNewIndexScene;
    public int lastPlayedSceneStatus;

    private void Awake()
    {
        if (setNewIndexScene)
        {
            PlayerPrefs.SetInt("LastPlayedScene", sceneIndex);
        }
    }

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("LastPlayedScene"));
    }
}
