using System;
using System.Collections;
using System.Collections.Generic;
using REWORK;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class OrderObject : MonoBehaviour
{
    [SerializeField] private Image _resultImage;
    [SerializeField] private List<Image> _ingredientsImages;
    [SerializeField] private ProgressBar progressBar;
    
    public CraftingRecipe Order { get; private set; }


    public Action<OrderObject> orderNotCompleted;

    private WaitForSeconds waitTime = new WaitForSeconds(GlobalSettings.ProgressBarTimeDecrease);
    
    public void InitOrder(CraftingRecipe order)
    {
        Order = order;
        
        _resultImage.sprite = Order.Result.Sprite;
        var itemNeeded = Order.ItemsNeeded;

        for (int i = 0; i < itemNeeded.Count; i++)
        {
            var image = _ingredientsImages[i];

                image.enabled = true;
                image.transform.parent.GetComponent<Image>().enabled = true;
                image.sprite = itemNeeded[i].Sprite;

        }
        progressBar.Init((float)order.CraftingTime);

       StartCoroutine(OrderTimer());
    }

    private IEnumerator OrderTimer()
    {
        var timeLeft = (float)Order.CraftingTime;
        while (timeLeft > 0)
        {
            yield return waitTime;
            timeLeft -= GlobalSettings.ProgressBarTimeDecrease;
            progressBar.UpdateBar(timeLeft, Order.CraftingTime);
        }

        OnTimerRunout();
    }

    private void OnTimerRunout()
    {
        orderNotCompleted?.Invoke(this);
        DestroyLogic(false);
    }

    public void OrderWasTurnedIn()
    {
        StopCoroutine(OrderTimer());
        DestroyLogic(true);
        
    }
    private void DestroyLogic(bool wasOk)
    {
        Destroy(gameObject);
    }  
}
