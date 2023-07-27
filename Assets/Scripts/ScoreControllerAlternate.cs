using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreControllerAlternate : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        RefreshUI();
    }

    public void DecreaseScore(int decrement)
    {
        score -= decrement;
        RefreshUI();
    }

    private void RefreshUI()
    {

        scoreText.text = "Score: " + score;
    }

    public void ResetUI()
    {
        scoreText.text = "Score: " + 0;
    }
}