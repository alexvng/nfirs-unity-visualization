using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider leftSlider, rightSlider;
    public DateTime minDate, maxDate;
    private DateTime globalmin, globalmax;
    private TimeSpan span;
    private float SPACING = 0.15f;

    private void Start()
    {
        globalmin = DateTime.Parse("01-01-2020");
        globalmax = DateTime.Parse("12-31-2023");
        span = globalmax - globalmin;
    }

    private void Update()
    {
        minDate = globalmin + (span * Mathf.Lerp(0,1,leftSlider.value));
        maxDate = globalmax - (span * Mathf.Lerp(1,0,rightSlider.value));

        //print($"{minDate}, {maxDate}");

        rightSlider.onValueChanged.AddListener((value) =>
        {
            if (leftSlider.value > rightSlider.value - SPACING)
            {
                leftSlider.value = rightSlider.value - SPACING;
            }
        });

        leftSlider.onValueChanged.AddListener(value =>
        {
            if (rightSlider.value < leftSlider.value + SPACING)
            {
                rightSlider.value = leftSlider.value + SPACING;
            }
        });
    }
}
