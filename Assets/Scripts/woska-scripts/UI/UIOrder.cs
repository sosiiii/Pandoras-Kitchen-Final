using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIOrder : MonoBehaviour
{
    [SerializeField] private Image _resultImage;
    [SerializeField] private List<Image> _ingredientsImages;
    [SerializeField] private ProgressBarBehavior _progressBarBehavior;
    public Order Order { get; private set; }


    public Action<UIOrder> onOrderNotCompleted;

    private void Awake()
    {

    }

    private void OnValidate()
    {
        //InitOrder(tmp);
    }

    public void InitOrder(Order order)
    {
        Order = order;
        
        _resultImage.sprite = Order.WhatWasOrdered.Result.itemSprite;
        var itemNeeded = Order.WhatWasOrdered.ItemsNeeded;
        
        Debug.Log(itemNeeded.Count);
        
        for (int i = 0; i < itemNeeded.Count; i++)
        {
            var image = _ingredientsImages[i];
            image.transform.parent.gameObject.SetActive(true);
            image.sprite = itemNeeded[i].itemSprite;
        }
        
        _progressBarBehavior.StartTimer(Order.TimeToAcceptOrder);
        
        _progressBarBehavior.OnTimerRunout += OnTimerRunout;
    }

    private void OnTimerRunout()
    {
        onOrderNotCompleted?.Invoke(this);
        DestroyLogic(false);
    }

    public void OrderWasTurnedIn()
    {
        _progressBarBehavior.StopTimer();
        DestroyLogic(true);
        
    }

    private void DestroyLogic(bool wasOk)
    {
        Destroy(gameObject);
    }  
}
