using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private TextMeshProUGUI scoreText = null;

    [Header("Battery Settings")]
    [SerializeField] private Image batteryFill = null;
    [SerializeField] private Color fullColor = Color.green,
                                   emptyColor = Color.red;

    private void Start()
    {
        ScoreManager.OnUpdatedScore += UpdateScoreText;
        BatteryManager.OnEnergyChange += UpdateBatteryFill;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateBatteryFill(float fillPercent)
    {
        batteryFill.fillAmount = fillPercent;
        batteryFill.color = Color.Lerp(emptyColor, fullColor, fillPercent - 0.15f);
    }
}