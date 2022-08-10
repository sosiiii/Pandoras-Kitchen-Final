using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using woska_scripts;

public class MightyZeus : MonoBehaviour
{
    [SerializeField] private Promt orderFinished;

    public static Action<String> zeusWantsToSpeak;
    private void OnEnable()
    {
        OrderController.orderFinished += OrderFinished;
        OrderGenerator.orderGenerated += OrderFinished;
    }

    private void OrderFinished()
    {
        var randomPrompt = orderFinished.GetRandomPrompt();
        Debug.Log(randomPrompt);
        zeusWantsToSpeak?.Invoke(randomPrompt);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
