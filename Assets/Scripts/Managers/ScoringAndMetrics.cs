using System;

public class TrackedScoreMetrics
{
    public TrackedScoreMetrics()
    {
        Score = 0;
        NumShots = 0;
        NumHits = 0;
        TimeElapsed = TimeSpan.Zero;
        startTime = DateTime.Now;
    }

    public int Score { get; private set; }
    public int NumShots { get; private set; }
    public int NumHits { get; private set; }
    public TimeSpan TimeElapsed { get; private set; }

    private DateTime startTime; // could also track last hit, last kill for multipliers

    internal void UpdateStats(ScoreType scoreType)
    {
        Score += (int)scoreType;

        switch (scoreType)
        {
            case ScoreType.Shot:
                ++NumShots;
                break;
            case ScoreType.Hit:
                ++NumHits;
                break;
            default:
                break;
        }
    }

    internal void CeaseTracking()
    {
        TimeElapsed = DateTime.Now - startTime;
    }
}

// Using an enum to ensure consistency in scoring across scripts
// Could make or use a dictionary serializer for cleaner key, value pairs that could be read in inspector
// There are definitely better ways to do this
public enum ScoreType // type = index (being used for point value as well right now)
{
    Shot = 1,
    HitMartyrdom = 3,
    Hit = 5,
    Destructible = 10,
    BuildingExplosion = 20,
    BasicEnemy = 25,
    HarderEnemy = 45,
    WowEnemy = 90
}