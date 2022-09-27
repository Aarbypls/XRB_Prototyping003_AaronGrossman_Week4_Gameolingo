using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score = 2600;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _scoreText2;
    [SerializeField] private List<MenuManager> _menuManagers;

    private void Start()
    {
        SetMainMenuScoreTexts();
    }

    private void SetMainMenuScoreTexts()
    {
        foreach (MenuManager menuManager in _menuManagers)
        {
            menuManager.UpdateScoreText(_score);
        }
    }

    public void AddToScore(int amountToAdd)
    {
        _score += amountToAdd;
        SetScoreTexts();
    }

    private void SetScoreTexts()
    {
        _scoreText.SetText(_score.ToString());
        _scoreText2.SetText("Score\n" + _score);
        SetMainMenuScoreTexts();
    }

    public int GetScore()
    {
        return _score;
    }

    public void ResetScore()
    {
        _score = 0;
        SetScoreTexts();
    }
}
