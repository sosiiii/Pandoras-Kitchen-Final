using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingProcessUI : MonoBehaviour
{
    [SerializeField] List<GameObject> cookingSteps = new List<GameObject>();
    private int stepIndex = 0;

    [SerializeField] GameObject controlsUI;
    [SerializeField] GameObject cookingUI;

    private void Start()
    {
        stepIndex = 0;
    }

    public void NewStep()
    {
        if (stepIndex >= cookingSteps.Count - 1) { SceneManager.LoadScene("Tutorial"); return;  }

        stepIndex++;
        cookingSteps[stepIndex].SetActive(true);
    }

    public void PreviousStep()
    {
        if (stepIndex == 0) { controlsUI.SetActive(true); cookingUI.SetActive(false); return; }

        cookingSteps[stepIndex].SetActive(false);
        stepIndex--;
    }
}
