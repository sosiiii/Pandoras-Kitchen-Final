using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private Image _barFill;


    public Action OnTimerRunout;


    private float remainingDuration;
    private float maxDuration;
    
    public bool IsRunning { get; private set; }

    private void Awake()
    {
        _barFill = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        ToggleActive(false);
    }

    public void StartTimer(float duration)
    {
        Debug.Log("WORRKS");
        remainingDuration = maxDuration = duration;
        StartCoroutine(UpdateTimer());
    }

    public void IncreaseDuration(float amount)
    {
        remainingDuration += amount;
        maxDuration += amount;
    }
    private IEnumerator UpdateTimer()
    {
        _barFill.fillAmount = 1-(remainingDuration/maxDuration);
        IsRunning = true;
        var timeDecrease = 0.1f;
        while (remainingDuration > 0)
        {
            
            yield return new WaitForSeconds(timeDecrease);
            remainingDuration -= timeDecrease;
            _barFill.fillAmount = 1-(remainingDuration/maxDuration);
        }
        IsRunning = false;
        OnTimerRunout.Invoke();
        ToggleActive(false);
    }

    public void ToggleActive(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
