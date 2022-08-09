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
    
    public enum ProgressBarType
    {
        Increase,
        Decrease
    }

    [SerializeField] private ProgressBarType type = ProgressBarType.Increase;

    private void Awake()
    {
        mask = transform.GetChild(0).GetComponent<Image>();
        fill = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        //background = GetComponent<Image>();
        
        fill.color = fillColorFull;
        
        mask.gameObject.SetActive(false);
    }
    public void Init(float maximum)
    {

        mask.gameObject.SetActive(true);
        _current = _maximum = maximum;
        
        var currentFill = GetCurrentFill();
        fill.fillAmount = currentFill;
    }
    public void UpdateBar(float current, float maximum)
    {
        _current = current;
        _maximum = maximum;
        
        var currentFill = GetCurrentFill();
        fill.fillAmount = currentFill;

        if (currentFill > 0.6f) 
            fill.color = fillColorFull;
        else if (currentFill > 0.3f)
            fill.color = fillColorHalf;
        else
            fill.color = fillColorLow;
    }

    public void StopTimer()
    {
        mask.gameObject.SetActive(false);
    }
    
    private float GetCurrentFill()
    {
        float currentOffset = _current - _minimum;
        float maximumOffset = _maximum - _minimum;

        var fill = currentOffset / maximumOffset;
        
        return type == ProgressBarType.Decrease ?  fill : 1 - fill;
    }
}
