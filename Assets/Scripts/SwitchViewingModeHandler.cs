using CesiumForUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SwitchViewingModeHandler : MonoBehaviour
{
    private bool freeFly;
    [SerializeField] private GameObject camera1, camera2, tooltip, tiles1, tiles2;
    private CesiumGlobeAnchor a1, a2;

    private void Start()
    {
        freeFly = false;
        a1 = camera1.GetComponent<CesiumGlobeAnchor>();
        a2 = camera2.GetComponent<CesiumGlobeAnchor>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            freeFly = !freeFly;
            
            
            var v = new double3(a1.longitudeLatitudeHeight.x, a1.longitudeLatitudeHeight.y, 340);
            a2.longitudeLatitudeHeight = v;
        }

        camera2.gameObject.SetActive(freeFly);
        camera1.gameObject.SetActive(!freeFly);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tiles1.gameObject.SetActive(true);
            tiles2.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tiles1.gameObject.SetActive(false);
            tiles2.gameObject.SetActive(true);
        }
    }
}
