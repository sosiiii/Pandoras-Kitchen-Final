using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using woska_scripts;
using Random = UnityEngine.Random;

public class MightyZeus : MonoBehaviour
{
    [SerializeField] private List<Promt> orderFinished;

    public static Action<String> zeusWantsToSpeak;
    private void OnEnable()
    {
        //OrderController.orderFinished += OrderFinished;
        
        OrderGenerator.orderGenerated += _ => OrderFinished(PromptContext.NewOrderGenerated);
        
        OrderController.orderFinished += () => OrderFinished(PromptContext.CompletedOrder);
        OrderController.wrongOrderTurnedIn += () => OrderFinished(PromptContext.WrongOrderTurnIn);
        OrderController.orderNotFinished += () => OrderFinished(PromptContext.NotCompletedOrder);
        

        Player.playerDeath += _ => OrderFinished(PromptContext.PlayerDeath);
    }

    private void OnDisable()
    {
        OrderGenerator.orderGenerated -= _ => OrderFinished(PromptContext.NewOrderGenerated);
        
        OrderController.orderFinished -= () => OrderFinished(PromptContext.CompletedOrder);
        OrderController.wrongOrderTurnedIn -= () => OrderFinished(PromptContext.WrongOrderTurnIn);
        OrderController.orderNotFinished -= () => OrderFinished(PromptContext.NotCompletedOrder);
        

        Player.playerDeath -= _ => OrderFinished(PromptContext.PlayerDeath);
    }

    private void OrderFinished(PromptContext promptContext)
    {
        if(Random.value > 0.6f) return;
        var prompt = orderFinished.Find(prompt => prompt.Context == promptContext);
        
        var randomPrompt = prompt.GetRandomPrompt();
        //Debug.Log(randomPrompt);
        zeusWantsToSpeak?.Invoke(randomPrompt);
    }
}
