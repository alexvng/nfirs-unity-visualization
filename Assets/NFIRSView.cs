using GeoJSON.Text.Feature;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFIRSView : MonoBehaviour
{
    [SerializeField] public Feature feature;
    private MeshRenderer m;
    private BoxCollider c;
    private DetectRaycast d;
    private SliderManager sliderManager;

    private void Start()
    {
        m = GetComponent<MeshRenderer>();
        c = m.GetComponent<BoxCollider>();
        d = m.GetComponent<DetectRaycast>();
        sliderManager = FindObjectOfType<SliderManager>();
    }

    private void Update()
    {
        var date = DateTime.Parse(feature.Properties["date"].ToString());
        var inWindow = date >= sliderManager.minDate && date <= sliderManager.maxDate;
        m.enabled = inWindow;
        c.enabled = inWindow;
        d.enabled = inWindow;
    }
}
