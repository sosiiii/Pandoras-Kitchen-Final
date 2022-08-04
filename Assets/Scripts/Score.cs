using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Game")]
    public int score;
    [SerializeField] int scoreToAddRemove;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private int oneStarScore;
    [SerializeField] private int twoStarsScore;
    [SerializeField] private int threeStarsScore;

    [Header("Results")]
    [SerializeField] private TextMeshProUGUI scoreTextResults;
    public List<GameObject> stars = new List<GameObject>();

    WinSystem winSystem;
        
    private void Awake()
    {
        winSystem = FindObjectOfType<WinSystem>();
    }

    private void Start()
    {
        score = 0;
    }

    //Increase score when order is finished
    public void IncreaseScore()
    {
        score += scoreToAddRemove;
    }

    //Decrease score when order is failed
    public void DecreaseScore()
    {
        score -= scoreToAddRemove;
    }

    void Update()
    {
        scoreText.text = score.ToString();
        scoreTextResults.text = score.ToString();
    }

    public IEnumerator ShowScoreResults()
    {
        if (score >= oneStarScore) {stars[0].GetComponent<Animator>().SetBool("FirstShowStar", true);}

        if (score >= twoStarsScore)
        {
            yield return new WaitForSeconds(1f);
            stars[1].GetComponent<Animator>().SetBool("SecondShowStar", true);
        }

        if (score >= threeStarsScore)
        {
            yield return new WaitForSeconds(1f);
            stars[2].GetComponent<Animator>().SetBool("ThirdShowStar", true);

            //level is won
            winSystem.LevelIsWon();
        }
    }
}
