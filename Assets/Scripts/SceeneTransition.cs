using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceeneTransition : MonoBehaviour
{
    [SerializeField] RectTransform fader;


    private void Awake()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 1).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }
}
