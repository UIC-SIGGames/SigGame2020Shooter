using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnEnd = delegate { };
    public static float DespawnTime { get; private set; }
    public static bool GameOver { get; private set; }

    [SerializeField] private float despawnTime = 4.0f;

    private void Start()
    {
        DespawnTime = despawnTime;
        GameOver = false;
        BatteryManager.OnEnergyChange += EndGame;
    }

    private void EndGame(float healthPercent)
    {
        if (healthPercent >= 0)
            return;
        if(!GameOver)
        {
            GameOver = true;
            Debug.Log("Game over");
            OnEnd();
        }
    }
}