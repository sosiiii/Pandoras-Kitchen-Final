using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using woska_scripts;

public class OrderController : MonoBehaviour
{

    public static Action orderRemoved;
    public static Action orderFinished;
    public static Action orderNotFinished;
    public static Action wrongOrderTurnedIn;

    [SerializeField] private OrderObject orderObjectItemPrefab;
    
    private List<OrderObject> _activeOrders = new List<OrderObject>();
    Score score;
    
    private void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    private void OnEnable()
    {
        OrderGenerator.orderGenerated += OnNewOrder;
        OrderReceiver.orderTurnedIn += OrderTurnIn;
    }
    private void OnDisable()
    {
        OrderGenerator.orderGenerated -= OnNewOrder;
    }
    private void OnNewOrder(CraftingRecipe order)
    {
        var orderUI = Instantiate(orderObjectItemPrefab, transform);
        orderUI.orderNotCompleted += OrderNotCompleted;
        
        orderUI.InitOrder(order);
        _activeOrders.Add(orderUI);
    }
    private void OrderNotCompleted(OrderObject orderObject)
    {
        // Lower score
        _activeOrders.Remove(orderObject);
        score.DecreaseScore();
        
        orderNotFinished?.Invoke();
        orderRemoved?.Invoke();
        //AddScore(scoreForCompletedOrder);
    }

    private void OrderTurnIn(Item item)
    {
        var foundMatchingOrder = false;
        foreach (var uiOrder in _activeOrders)
        {

            var goal = uiOrder.Order.Result;
            if (goal == item)
            {
                foundMatchingOrder = true;
                
                uiOrder.OrderWasTurnedIn();
                
                _activeOrders.Remove(uiOrder);
                break;
            }

        }

        if (foundMatchingOrder)
        {
            //Score up
            score.IncreaseScore();
            //AddScore(scoreForCompletedOrder);
            orderFinished?.Invoke();
        }
        else
        {
            //Score down
            score.DecreaseScore();
            //AddScore(_penaltyForTurningInBadOrder);
            wrongOrderTurnedIn?.Invoke();
            return;
        }
        orderRemoved?.Invoke();

    }
}
