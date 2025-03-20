using GeoJSON.Text.Feature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using GeoJSON.Text.Geometry;
using CesiumForUnity;
using Unity.Mathematics;

/// <summary>
/// Given a GeoJSON file that contains NFIRS 'fireincident' data, spawn one marker per fire event
/// </summary>
public class PopulateGeoJSONData : MonoBehaviour
{
    [SerializeField] private TextAsset geoJSONFile;
    [SerializeField] private GameObject fireMarkerPrefab;
    [SerializeField, Tooltip("Set lower to troubleshoot lag. Remember, DON'T put CesiumOriginShift on markers!")]
    private int MAX_MARKERS_ALLOWED = 2000;

    private void Start()
    {
        string json = geoJSONFile.text;
        GeoJSON.Text.Feature.FeatureCollection fc = JsonSerializer.Deserialize<FeatureCollection>(json);

        ProcessJSON(fc);
    }

    /// <summary>
    /// Extract each Feature->Geometry->Point from JSON
    /// </summary>
    void ProcessJSON(FeatureCollection fc)
    {
        var i = 0;
        foreach (var f in fc.Features)
        {
            Point p = (Point)f.Geometry;
            var c = p.Coordinates;
            var coords = new double3(c.Latitude, c.Longitude, 340); // remember lat/lon are transposed!

            var marker = Instantiate(fireMarkerPrefab);
            CesiumGlobeAnchor anchor = marker.GetComponent<CesiumGlobeAnchor>();

            marker.transform.SetParent(this.transform); // parent to this obj, which has the OriginShift
            anchor.longitudeLatitudeHeight = coords; // move to position on map
            marker.GetComponent<MarkerDataContainer>().feature = f; // give marker access to its own data
            
            marker.SetActive(true);
            
            i++;
            if (i >= MAX_MARKERS_ALLOWED) break; // lag safety break
        }
    }

}
