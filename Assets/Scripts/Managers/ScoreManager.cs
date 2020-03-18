using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static event Action<int> OnUpdatedScore = delegate { }; // Updates UIManager

    TrackedScoreMetrics metrics = new TrackedScoreMetrics();

    private void Start()
    {
        Instance = this;
        GameManager.OnEnd += WrapUp;
    }

    public void AddPoints(ScoreType scoreType)
    {
        metrics.UpdateStats(scoreType);
        OnUpdatedScore(metrics.Score);
    }

    private void WrapUp()
    {
        metrics.CeaseTracking();
        Debug.Log("Use " + metrics.TimeElapsed.TotalSeconds + " seconds as a statistic and save it!");
        Debug.Log("Shot " + metrics.NumShots + " and hit " + metrics.NumHits + " resulting in accuracy: " + 100 * ((float)metrics.NumHits / metrics.NumShots) + "%");
        Debug.Log("Use and save score statistics");
        // probably send the whole tracked metrics object to a UI screen for parsing
    }
}