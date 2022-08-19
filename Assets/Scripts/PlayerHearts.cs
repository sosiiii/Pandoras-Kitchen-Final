using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHearts : MonoBehaviour
{
    [SerializeField] Image[] heartsImages;
    [SerializeField] CanvasGroup heartHolder;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideHPSlowly());
    }

    public void UpdateLifeVisual(int lifeCount)
    {
        for (int i = 0; i < lifeCount; i++)
        {
            heartsImages[i].enabled = true;
        }

        heartHolder.alpha = 1f;

        StartCoroutine(HideHPSlowly());
    }

    private void HideHP()
    {
        for (int i = 0; i < heartsImages.Length; i++)
        {
            heartsImages[i].enabled = false;
        }
    }

    IEnumerator HideHPSlowly()
    {
        yield return new WaitForSeconds(0.2f);

        while (heartHolder.alpha > 0)
        {
            heartHolder.alpha -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        HideHP();
    }
}
