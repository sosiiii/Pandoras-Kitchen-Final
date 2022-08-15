using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZeusUI : MonoBehaviour
{
    [SerializeField] private GameObject promptPrefab;
    
    private bool isRunning = false;

    private Stack<string> prompts = new Stack<string>();

    private void OnEnable()
    {
        MightyZeus.zeusWantsToSpeak += ZeusWantsToSpeak;
    }

    private void OnDisable()
    {
        MightyZeus.zeusWantsToSpeak -= ZeusWantsToSpeak;
    }

    private void Awake()
    {
        promptPrefab.SetActive(false);
    }

    private void ZeusWantsToSpeak(string obj)
    {
        prompts.Push(obj);

        if (!isRunning)
            StartCoroutine(SpeechBubble());

    }

    private IEnumerator SpeechBubble()
    {
        isRunning = true;
        promptPrefab.SetActive(true);
        while (prompts.Count > 0)
        {
            promptPrefab.GetComponentInChildren<TextMeshProUGUI>().text = prompts.Pop();
            yield return new WaitForSeconds(10f);
        }
        promptPrefab.SetActive(false);
        isRunning = false;
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
