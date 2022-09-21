using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _scoreText2;
    
    public void AddToScore(int amountToAdd)
    {
        score += amountToAdd;
        SetScoreTexts();
    }

    private void SetScoreTexts()
    {
        _scoreText.SetText(score.ToString());
        _scoreText2.SetText("Score\n" + score);
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        SetScoreTexts();
    }
}
