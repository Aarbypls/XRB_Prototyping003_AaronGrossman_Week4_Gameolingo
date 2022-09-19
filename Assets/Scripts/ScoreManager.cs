using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private TextMeshProUGUI _scoreText;
    
    public void AddToScore(int amountToAdd)
    {
        score += amountToAdd;
        _scoreText.SetText(score.ToString());
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        _scoreText.SetText(score.ToString());
    }
}
