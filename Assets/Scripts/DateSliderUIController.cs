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
    private float SLIDER_SPACING = 0.075f;

    [SerializeField] private Slider leftSlider, rightSlider;
    [SerializeField] private TMP_Text topText;

    private const string START_DATE = "01-01-2020";
    private const string END_DATE = "12-31-2023";

    public DateTime minDateFiltered, maxDateFiltered;
    private DateTime globalMinDatestamp, globalMaxDatestamp;
    private TimeSpan fullTimespan;

    private void Start()
    {
        globalMinDatestamp = DateTime.Parse(START_DATE);
        globalMaxDatestamp = DateTime.Parse(END_DATE);
        fullTimespan = globalMaxDatestamp - globalMinDatestamp;
    }

    private void Update()
    {
        minDateFiltered = globalMinDatestamp + (fullTimespan * Mathf.Lerp(0, 1, leftSlider.value));
        maxDateFiltered = globalMaxDatestamp - (fullTimespan * Mathf.Lerp(1, 0, rightSlider.value));

        topText.text = $"{minDateFiltered.ToString("MMM d yyyy")}—{maxDateFiltered.ToString("MMM d yyyy")}";

        rightSlider.onValueChanged.AddListener((value) =>
        {
            if (leftSlider.value > rightSlider.value - SLIDER_SPACING)
            {
                leftSlider.value = rightSlider.value - SLIDER_SPACING;
            }
        });

        leftSlider.onValueChanged.AddListener(value =>
        {
            if (rightSlider.value < leftSlider.value + SLIDER_SPACING)
            {
                rightSlider.value = leftSlider.value + SLIDER_SPACING;
            }
        });
    }
}
