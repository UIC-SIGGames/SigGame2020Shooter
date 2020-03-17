using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static event Action<int> OnUpdatedScore = delegate { };

    private int currentScore = 0;

    private void Start()
    {
        Instance = this;
    }

    public void AddPoints(int numPoints)
    {
        currentScore += numPoints;
        OnUpdatedScore(currentScore);
    }
}
