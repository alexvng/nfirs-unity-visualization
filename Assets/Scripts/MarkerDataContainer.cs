using GeoJSON.Text.Feature;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

/// <summary>
/// Stores instance of GeoJSON.Text.Feature,
/// which contains the data of a single building fire.
/// </summary>
public class MarkerDataContainer : MonoBehaviour
{
    [SerializeField] public Feature feature;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private FireMarkerDetectRaycast raycastDetector;
    private DateSliderUIController sliderUIController;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = meshRenderer.GetComponent<BoxCollider>();
        raycastDetector = meshRenderer.GetComponent<FireMarkerDetectRaycast>();
        sliderUIController = FindObjectOfType<DateSliderUIController>();

        // For all lines that start with TEXT:
        // replace with <b><u>TEXT:</b></u>
        string pattern = @"^(.*?): (.*)$";
        string replacement = @"<b><u>$1:</u></b> $2";
        var s = Regex.Replace(feature.Properties["description"].ToString(), pattern, replacement, RegexOptions.Multiline);
        feature.Properties["description"] = s;
    }

    // needs to run late so that FireMarkerDetectRaycast can handle sizing first
    // else pop-in glitching occurs
    private void LateUpdate()
    {
        var date = DateTime.Parse(feature.Properties["date"].ToString());
        var isInWindow = date >= sliderUIController.minDateOnSlider && date <= sliderUIController.maxDateOnSlider;
        meshRenderer.enabled = isInWindow;
        boxCollider.enabled = isInWindow;
        raycastDetector.enabled = isInWindow;
    }
}
