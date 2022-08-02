using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        FullscreenStart();
        VolumeStart();
    }

    private void Update()
    {
        ChangeFullscreen();
    }

    //FULLSCREEN
    private void FullscreenStart()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            fullscreenToggle.isOn = true;
            Screen.fullScreen = true;
        }

        if (PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            fullscreenToggle.isOn = false;
            Screen.fullScreen = false;
        }
    }
    public void ChangeFullscreen()
    {
        if (fullscreenToggle.isOn == true)
        {
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("Fullscreen" , 1);
        }
        
        else if (fullscreenToggle.isOn == false)
        {
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }
    //FULLSCREEN

    //VOLUME
    private void VolumeStart()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
            LoadVolume();
        }

        else
        {
            LoadVolume();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }
    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }
    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
    //VOLUME
}