using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UvodScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadMainMenu),5f);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
