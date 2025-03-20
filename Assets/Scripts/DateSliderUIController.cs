using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Constrains the positions and values of the slider at the top of game window.
/// 
/// Slider allows you to change the date range viewed, from Jan 2020 to Dec 2023
/// (all published NFIRS data from Tempe at this program's creation).
/// </summary>
public class DateSliderUIController : MonoBehaviour
{
    /// <summary>
    /// How far apart can the slider handles be from each other before locking out
    /// </summary>
    private float SLIDER_SPACING = 0.025f;

    [SerializeField] private Slider leftSlider, rightSlider;
    [SerializeField] private TMP_Text dateText;

    //TODO make serializable when data expands
    private const string START_DATE = "01-01-2020";
    private const string END_DATE = "12-31-2023";

    public DateTime minDateOnSlider, maxDateOnSlider;
    private DateTime globalMinDate, globalMaxDate;
    private TimeSpan startToEndTimespan;

    private void Start()
    {
        globalMinDate = DateTime.Parse(START_DATE);
        globalMaxDate = DateTime.Parse(END_DATE);
        startToEndTimespan = globalMaxDate - globalMinDate;
        rightSlider.onValueChanged.AddListener((value) =>
        {
            if (leftSlider.value > rightSlider.value - SLIDER_SPACING)
            {
                leftSlider.value = rightSlider.value - SLIDER_SPACING;
            }
            UpdateValues();
        });

        leftSlider.onValueChanged.AddListener(value =>
        {
            if (rightSlider.value < leftSlider.value + SLIDER_SPACING)
            {
                rightSlider.value = leftSlider.value + SLIDER_SPACING;
            }
            UpdateValues();
        });

        UpdateValues(); // run once to show
    }
    
    private void UpdateValues()
    {
        minDateOnSlider = globalMinDate + (startToEndTimespan * Mathf.Lerp(0, 1, leftSlider.value));
        maxDateOnSlider = globalMaxDate - (startToEndTimespan * Mathf.Lerp(1, 0, rightSlider.value));

        dateText.text = $"{minDateOnSlider.ToString("MMM d yyyy")}—{maxDateOnSlider.ToString("MMM d yyyy")}";

    }
}
