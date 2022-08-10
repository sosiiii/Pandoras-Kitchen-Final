using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZeusUI : MonoBehaviour
{
    [SerializeField] private GameObject promptPrefab;

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
        promptPrefab.SetActive(true);
        promptPrefab.GetComponentInChildren<TextMeshProUGUI>().text = obj;
        Invoke(nameof(HidePrompt), 1f);
    }

    private void HidePrompt()
    {
        promptPrefab.SetActive(false);
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
