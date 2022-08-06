using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSave : MonoBehaviour
{
    void Awake()
    {
        FullscreenStart();
        VolumeStart();
    }

    //FULLSCREEN
    private void FullscreenStart()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            Screen.fullScreen = true;
        }

        if (PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            Screen.fullScreen = false;
        }
    }

    //VOLUME
    private void VolumeStart()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }
}
