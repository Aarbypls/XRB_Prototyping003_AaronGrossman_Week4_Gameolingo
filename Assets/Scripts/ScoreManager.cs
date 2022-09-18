using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore(int amountToAdd)
    {
        score += amountToAdd;
        Debug.Log("Score: " + GetScore());
    }

    public int GetScore()
    {
        return score;
    }
}
