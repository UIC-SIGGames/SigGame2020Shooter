using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If we want to do Earthbound style health we could make a callback when fillAmount == 0.
public class HealthBar : MonoBehaviour
{
    [SerializeField] private float interpSpeed = .3f; // speed the bar moves... could change it to reflect severity?
    [SerializeField] private Color startColor  = Color.green,
                                   endColor    = Color.red;

    private float fillAmount        = 1,
                  desiredFillAmount = 1;
    private bool  reachedFill       = true,
                  decreasing;

    private Color currentColor;
    private Material material;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (!reachedFill)
            UpdateFill();
    }

    public void SetFill(float newFillAmount)
    {
        if (newFillAmount > 1)
            newFillAmount = 1;

        fillAmount = newFillAmount;
        UpdateVisualProperties();
    }

    public void ChangeFill(float newFillAmount)
    {
        if (newFillAmount < 0)
            newFillAmount = 0;
        else if (newFillAmount > 1)
            newFillAmount = 1;

        decreasing = (fillAmount > newFillAmount) ? true : false;
        reachedFill = false;
        desiredFillAmount = newFillAmount;
    }

    private void UpdateFill()
    {
        fillAmount += (decreasing) ? -interpSpeed * Time.deltaTime 
                                   :  interpSpeed * Time.deltaTime;

        if (decreasing && fillAmount <= desiredFillAmount)
            reachedFill = true;
        else if (!decreasing && fillAmount >= desiredFillAmount)
        {
            if (fillAmount > 1)
                fillAmount = 1;

            reachedFill = true;
        }

        UpdateVisualProperties();
    }

    private void UpdateVisualProperties()
    {
        material.SetFloat("Vector1_8ECE0250", fillAmount);
        currentColor = Color.Lerp(endColor, startColor, fillAmount - .1f);
        material.SetColor("Color_55B1CAA3", currentColor);
    }
}
