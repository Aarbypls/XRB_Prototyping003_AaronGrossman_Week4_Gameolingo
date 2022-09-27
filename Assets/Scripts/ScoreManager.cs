using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _scoreText2;
    [SerializeField] private List<MenuManager> _menuManagers;

    private void Start()
    {
        SetMainMenuScoreTexts(0);
    }

    private void SetMainMenuScoreTexts(int amountToAdd)
    {
        foreach (MenuManager menuManager in _menuManagers)
        {
            menuManager.UpdateScoreText(amountToAdd);
        }
    }

    public void AddToScore(int amountToAdd)
    {
        _score += amountToAdd;
        SetScoreTexts(amountToAdd);
    }

    private void SetScoreTexts(int amountToAdd)
    {
        _scoreText.SetText(_score.ToString());
        _scoreText2.SetText("Score\n" + _score);
        SetMainMenuScoreTexts(amountToAdd);
    }

    public int GetScore()
    {
        return _score;
    }

    public void ResetScore()
    {
        _score = 0;
        SetScoreTexts(0);
    }
}
