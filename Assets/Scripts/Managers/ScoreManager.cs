using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static event Action<int> OnUpdatedScore = delegate { };

    // Tuple for some basic metrics, if we need more we can use an object
    private (int score, int numShots, int numHits, TimeSpan elapsedTime) TrackedMetrics = (0, 0, 0, TimeSpan.Zero);

    private DateTime startTime; // could also track last hit, last kill for multipliers
    private void Start()
    {
        Instance = this;
        GameManager.OnEnd += WrapUp;
        startTime = DateTime.Now;
    }

    public void AddPoints(ScoreType scoreType)
    {
        TrackExtraMetrics(scoreType);

        TrackedMetrics.score += (int)scoreType;
        OnUpdatedScore(TrackedMetrics.score);
    }

    private void WrapUp()
    {
        TrackedMetrics.elapsedTime = DateTime.Now - startTime;

        Debug.Log("Use " + TrackedMetrics.elapsedTime.TotalSeconds + " seconds as a statistic and save it!");
        Debug.Log("Shot " + TrackedMetrics.numShots + " and hit " + TrackedMetrics.numHits + " resulting in accuracy: " + 100 * ((float)TrackedMetrics.numHits / TrackedMetrics.numShots) + "%");
        Debug.Log("Use and save score statistics");
        // probably send the whole tracked metrics object to a UI screen for parsing
    }

    private void TrackExtraMetrics(ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.Shot:
                ++TrackedMetrics.numShots;
                break;
            case ScoreType.Hit:
                ++TrackedMetrics.numHits;
                break;
            default:
                break;
        }
    }
}

// Using an enum to ensure consistency in scoring across scripts
// Could make or use a dictionary serializer for cleaner key, value pairs that could be read in inspector
// There are definitely better ways to do this
public enum ScoreType // type = index (being used for point value as well right now)
{
    Shot = 1,
    Hit = 5,
    Destructible = 10,
    BuildingExplosion = 20,
    BasicEnemy = 25,
    HarderEnemy = 45,
    WowEnemy = 90
}