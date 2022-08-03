using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue;
    public TextMeshProUGUI timerText;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Image fillImage;
    private float timeSliderMaxValue;

    [SerializeField] private Color32 greenColor;
    [SerializeField] private Color32 orangeColor;
    [SerializeField] private Color32 redColor;

    private void Start()
    {
        fillImage.color = greenColor;
        timeSliderMaxValue = timeValue;
        timeSlider.maxValue = timeSliderMaxValue;
    }

    void Update()
    {
        ChangeColorOfSlider();

        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }

        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);

        timeSlider.value = timeValue;
    }

    private void ChangeColorOfSlider()
    {
        if (timeSlider.value <= timeSliderMaxValue / 2)
        {
            if (timeSlider.value >= timeSliderMaxValue / 3)
            {
                fillImage.color = orangeColor;
            }

            else
            {
                fillImage.color = redColor;
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
