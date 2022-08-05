using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using woska_scripts;

public class UIOrderController : MonoBehaviour
{

    private List<UIOrder> _orderUis = new List<UIOrder>();
    [SerializeField] private UIOrder uiOrderPrefab;
    private void OnEnable()
    {
        OrderGenerator.onOrderAccepted += OnOrderAccepted;
        OrderReceiver.onOrderTurnedIn += OrderTurnIn;
    }
    private void OnDisable()
    {
        OrderGenerator.onOrderAccepted -= OnOrderAccepted;
    }
    private void OnOrderAccepted(Order order)
    {
        var orderUI = Instantiate(uiOrderPrefab, transform);

        orderUI.onOrderNotCompleted += OrderNotCompleted;
        orderUI.InitOrder(order);


        _orderUis.Add(orderUI);
    }

    private void OrderNotCompleted(UIOrder order)
    {
      /// Lower score
      _orderUis.Remove(order);
    }

    private void OrderTurnIn(Item item)
    {
        var foundMatchingOrder = false;
        foreach (var uiOrder in _orderUis)
        {

            var goal = uiOrder.Order.WhatWasOrdered.Result;
            if (goal == item)
            {
                foundMatchingOrder = true;
                
                uiOrder.OrderWasTurnedIn();
                
                _orderUis.Remove(uiOrder);
                break;
            }

        }

        if (foundMatchingOrder)
        {
            //Score up
        }
        else
        {
            //Score down
        }
    }
    
    
    
}
