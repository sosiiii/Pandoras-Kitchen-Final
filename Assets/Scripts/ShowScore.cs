using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    [SerializeField] private Score score;

    void Start()
    {
        score.ShowScoreResults();
    }
}
