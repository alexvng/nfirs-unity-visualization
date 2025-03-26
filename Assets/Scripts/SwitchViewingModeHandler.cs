using CesiumForUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Handle the hotkeys for switching map modes (top-down vs freefly, Google vs Bing)
/// </summary>
public class SwitchViewingModeHandler : MonoBehaviour
{
    private bool isFreeFlyActive;
    [SerializeField] private GameObject topDownCamera, freeFlyCamera, googleTiles, bingTiles;
    private CesiumGlobeAnchor anchor1, anchor2;
    [SerializeField] private GameObject topDownControlsText, freeFlyControlsText;

    private void Start()
    {
        isFreeFlyActive = false;
        anchor1 = topDownCamera.GetComponent<CesiumGlobeAnchor>();
        anchor2 = freeFlyCamera.GetComponent<CesiumGlobeAnchor>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFreeFlyActive = !isFreeFlyActive; // flip bool

            //TODO fix magic number (340 is good ground height for cesium)
            //IDK what unit it's in
            var v = new double3(anchor1.longitudeLatitudeHeight.x, anchor1.longitudeLatitudeHeight.y, 340);
            anchor2.longitudeLatitudeHeight = v;
        }

        freeFlyCamera.gameObject.SetActive(isFreeFlyActive);
        freeFlyControlsText.gameObject.SetActive(isFreeFlyActive);
        topDownCamera.gameObject.SetActive(!isFreeFlyActive);
        topDownControlsText.gameObject.SetActive(!isFreeFlyActive);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            googleTiles.gameObject.SetActive(true);
            bingTiles.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            googleTiles.gameObject.SetActive(false);
            bingTiles.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}
