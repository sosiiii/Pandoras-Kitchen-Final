using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingProcessUI : MonoBehaviour
{
    [SerializeField] List<GameObject> cookingSteps = new List<GameObject>();
    private int stepIndex = 0;

    public void NewStep()
    {
        if (stepIndex >= cookingSteps.Count - 1) { SceneManager.LoadScene("TUTORIAL"); return;  }

        stepIndex++;
        cookingSteps[stepIndex].SetActive(true);
    }
}
