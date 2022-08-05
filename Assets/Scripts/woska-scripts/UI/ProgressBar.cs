using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private float _minimum;

    private float _maximum;

    private float _current;


    private Image mask;
    private Image fill;
    private Image background;

    [SerializeField] private Color fillColorFull;
    [SerializeField] private Color fillColorHalf;
    [SerializeField] private Color fillColorLow;
    
    public bool IsRunning { get; private set; }
    
    public Action OnTimerRunout;

    private void Awake()
    {
        mask = transform.GetChild(0).GetComponent<Image>();
        fill = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        background = GetComponent<Image>();
        ToggleActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        fill.color = fillColorFull;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartTimer(float duration)
    {
        _minimum = 0;
        _current = _maximum = duration;
        
        ToggleActive(true);
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        IsRunning = true;
        var timeDecrease = 0.1f;
        while (_current > _minimum)
        {
            
            yield return new WaitForSeconds(timeDecrease);
            _current -= timeDecrease;
            var currentFill = GetCurrentFill();
            fill.fillAmount = currentFill;

            if (currentFill > 0.6f)
            {
                fill.color = fillColorFull;
            }
            else if (currentFill > 0.3f)
                fill.color = fillColorHalf;
            else
            {
                fill.color = fillColorLow;
            }
        }
        IsRunning = false;
        OnTimerRunout?.Invoke();
        
        ToggleActive(false);
    }


    public void UpdateBar(float time)
    {
        _maximum += time;
        _current += time;
    }

    public void StopTimer()
    {
        StopCoroutine(UpdateTimer());
    }

    public void ToggleActive(bool toggle)
    {
        Debug.Log("Bar is: " + toggle);
        mask.enabled = toggle;
        fill.enabled = toggle;
        background.enabled = toggle;
        
        Debug.Log(background.name);
    }

    private float GetCurrentFill()
    {
        float currentOffset = _current - _minimum;
        float maximumOffset = _maximum - _minimum;

        return  currentOffset / maximumOffset;
    }
}
