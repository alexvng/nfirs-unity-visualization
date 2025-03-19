using CesiumForUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FireMarkerManager : MonoBehaviour
{
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private NFIRSDataPoint[] mydata;
    [SerializeField] private GameObject fireMarkerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        mydata = NFIRSDataPoint.CreateFromJSON(jsonFile);

        var i = 0;
        foreach (var point in mydata)
        {
            var marker = Instantiate(fireMarkerPrefab);
            marker.transform.SetParent(this.transform);
            CesiumGlobeAnchor anchor = marker.GetComponent<CesiumGlobeAnchor>();
            marker.transform.position = Vector3.zero;
            anchor.longitudeLatitudeHeight = new Unity.Mathematics.double3(point.lng, point.lat, point.elevation);
            marker.GetComponent<NFIRSView>().data = point;
            marker.SetActive(true);
            i++;
            //if (i >= 100) break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}