using GeoJSON.Text.Feature;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

public class MarkerDataContainer : MonoBehaviour
{
    [SerializeField] public Feature feature;
    private MeshRenderer m;
    private BoxCollider c;
    private FireMarkerDetectRaycast d;
    private DateSliderUIController sliderManager;

    private void Start()
    {
        m = GetComponent<MeshRenderer>();
        c = m.GetComponent<BoxCollider>();
        d = m.GetComponent<FireMarkerDetectRaycast>();
        sliderManager = FindObjectOfType<DateSliderUIController>();

        string pattern = @"^(.*?): (.*)$";
        string replacement = @"<b><u>$1:</u></b> $2";

        var s = Regex.Replace(feature.Properties["description"].ToString(), pattern, replacement, RegexOptions.Multiline);
        feature.Properties["description"] = s;
    }

    private void LateUpdate()
    {
        var date = DateTime.Parse(feature.Properties["date"].ToString());
        var inWindow = date >= sliderManager.minDateFiltered && date <= sliderManager.maxDateFiltered;
        m.enabled = inWindow;
        c.enabled = inWindow;
        d.enabled = inWindow;
    }
}
