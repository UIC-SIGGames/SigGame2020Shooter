using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static event Action<int> OnUpdatedScore = delegate { }; // Updates UIManager

    private bool usedMods = false;
    TrackedScoreMetrics metrics = new TrackedScoreMetrics();

    private void Start()
    {
        Instance = this;
        GameManager.OnEnd += WrapUp;
    }

    private void OnDestroy()
    {
        GameManager.OnEnd -= WrapUp;
    }

    public void InvalidateStats()
    {
        usedMods = true;
    }

    public void AddPoints(ScoreType scoreType)
    {
        if (!usedMods)
        {
            metrics.UpdateStats(scoreType);
            OnUpdatedScore(metrics.Score);
        }
    }

    public void TrackPeripheralMetrics(MetricType metric)
    {
        if (!usedMods) 
            metrics.UpdatePeripheralStats(metric);
    }

    private void WrapUp()
    {
        metrics.CeaseTracking();
        StartCoroutine(SendMetrics());
        Debug.Log("Use " + metrics.TimeElapsed.TotalSeconds + " seconds as a statistic and save it!");
        Debug.Log("Shot " + metrics.NumShots + " and hit " + metrics.NumHits + " resulting in accuracy: " + 100 * ((float)metrics.NumHits / metrics.NumShots) + "%");
        Debug.Log("Use and save score statistics");
        // probably send the whole tracked metrics object to a UI screen for parsing
    }

    private const string MetricsURL = "https://builds.topher.games/ACMS2020_ShootGame/RogueShootGameAddStats.php?";
    private IEnumerator SendMetrics() // Sends metrics to a MYSQL database
    {
        if (usedMods)
            yield break;

        UnityWebRequest UpdateOnlineMetrics = new UnityWebRequest
            (MetricsURL
            + "Shots=" + metrics.NumShots
            + "&Hits=" + metrics.NumHits
            + "&Playtime=" + (int)metrics.TimeElapsed.TotalSeconds
            + "&BatSpawned=" + metrics.NumBatsSpawned
            + "&BatRetrieved=" + metrics.NumBatsRetrieved
            + "&EnemiesSpawned=" + metrics.NumEnemiesSpawned
            + "&EnemiesKilled=" + metrics.NumEnemiesKilled
            + "&Score=" + metrics.Score
            + "&GrenadesThrown=" + metrics.NumGrenadesThrown);

        if (Application.platform != RuntimePlatform.WebGLPlayer)
            UpdateOnlineMetrics.SetRequestHeader("User-Agent", "Unity 2019");

        UpdateOnlineMetrics.SendWebRequest();

        yield return UpdateOnlineMetrics.isDone;

        if (UpdateOnlineMetrics.error != null)
            Debug.Log(UpdateOnlineMetrics.error);
        else
            Debug.Log("Metrics sent");
    }
}