using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotTimer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Image _barFill;


    public Action OnTimerRunout;


    private float remainingDuration;
    private float maxDuration;
    
    public bool IsRunning { get; private set; }
    
    public void StartTimer(float duration)
    {
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
        IsRunning = true;
        var timeDecrease = 0.1f;
        while (remainingDuration > 0)
        {
            
            yield return new WaitForSeconds(timeDecrease);
            remainingDuration -= timeDecrease;
            Debug.Log(remainingDuration);

            _barFill.fillAmount = 1-(remainingDuration/maxDuration);

        }
        IsRunning = false;
        OnTimerRunout.Invoke();
    }

    public void ToggleActive(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
