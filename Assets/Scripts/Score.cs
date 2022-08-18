using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Game")]
    public int score;
    [SerializeField] int scoreToAdd;
    [SerializeField] int scoreToRemove;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private int oneStarScore;
    [SerializeField] private int twoStarsScore;
    [SerializeField] private int threeStarsScore;

    public List<Image> starsImages = new List<Image>();

    [Header("Results")]
    [SerializeField] private TextMeshProUGUI scoreTextResults;
    public List<GameObject> stars = new List<GameObject>();

    WinSystem winSystem;
        
    private void Awake()
    {
        winSystem = FindObjectOfType<WinSystem>();
    }

    //Increase score when order is finished
    public void IncreaseScore()
    {
        score += scoreToAdd;
    }

    //Decrease score when order is failed
    public void DecreaseScore()
    {
        score -= scoreToRemove;

        if (score <= 0)
        {
            score = 0;
        }
    }

    void Update()
    {
        scoreText.text = score.ToString();
        scoreTextResults.text = score.ToString();

        ShowStarsInGame();
    }

    public void ShowStarsInGame()
    {
        if (score >= oneStarScore) { starsImages[0].color = Color.white; }
        else { starsImages[0].color = Color.black; }

        if (score >= twoStarsScore) { starsImages[1].color = Color.white; }
        else { starsImages[1].color = Color.black; }

        if (score >= threeStarsScore) { starsImages[2].color = Color.white; }
        else { starsImages[2].color = Color.black; }
    }

    public IEnumerator ShowScoreResults()
    {
        if (score >= oneStarScore) {stars[0].GetComponent<Animator>().SetBool("FirstShowStar", true);}

        if (score >= twoStarsScore)
        {
            yield return new WaitForSeconds(1f);
            stars[1].GetComponent<Animator>().SetBool("SecondShowStar", true);

            //unlock next level
            winSystem.LevelIsWon();
        }

        if (score >= threeStarsScore)
        {
            yield return new WaitForSeconds(1f);
            stars[2].GetComponent<Animator>().SetBool("ThirdShowStar", true);
        }
    }
}
