using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnEnd = delegate { };

    public static bool gameOver = false;

    private void Start()
    {
        BatteryManager.OnEnergyChange += EndGame;
    }

    private void EndGame(float healthPercent)
    {
        if (healthPercent >= 0)
            return;
        if(!gameOver)
        {
            gameOver = true;
            Debug.Log("Game over");
            OnEnd();
        }
    }
}