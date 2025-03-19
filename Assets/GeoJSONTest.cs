using GeoJSON.Text.Feature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeoJSON.Text;
using System.Text.Json;
using GeoJSON.Text.Geometry;
using CesiumForUnity;
using Unity.Mathematics;


public class GeoJSONTest : MonoBehaviour
{
    [SerializeField] private TextAsset geoJSON;
    [SerializeField] private GameObject fireMarkerPrefab;
    [SerializeField] private int MAX_RENDER;

    private void Start()
    {
        string json = geoJSON.text;
        FeatureCollection fc = JsonSerializer.Deserialize<FeatureCollection>(json);

        ProcessJSON(fc);
    }

    async void ProcessJSON(FeatureCollection fc)
    {
        var i = 0;
        foreach (var f in fc.Features)
        {
            Point p = (Point)f.Geometry;
            var c = p.Coordinates;
            var d = new double3(c.Longitude, c.Latitude, 0);
            //print(d);

            var marker = Instantiate(fireMarkerPrefab);
            marker.transform.SetParent(this.transform);
            CesiumGlobeAnchor anchor = marker.GetComponent<CesiumGlobeAnchor>();
            marker.transform.position = Vector3.zero;

            Cesium3DTileset tileset = FindAnyObjectByType<Cesium3DTileset>();
            CesiumSampleHeightResult result = await tileset.SampleHeightMostDetailed(d);
            print(result.longitudeLatitudeHeightPositions[0]);
            anchor.longitudeLatitudeHeight = result.longitudeLatitudeHeightPositions[0];
            print(anchor.longitudeLatitudeHeight);
            marker.GetComponent<NFIRSView>().feature = f;
            marker.SetActive(true);
            i++;
            if (i >= MAX_RENDER) break;
        }
    }

}
