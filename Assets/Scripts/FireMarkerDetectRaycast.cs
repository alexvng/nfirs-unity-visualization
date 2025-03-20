using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handle raycast detection and other Update() tasks for each marker
/// </summary>
public class FireMarkerDetectRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private TMP_Text tooltip;

    private void Start()
    {
        tooltip = GameObject.FindWithTag("tooltip_text").GetComponent<TMP_Text>();
    }

    //TODO: Major lag contribution. Need refactor to a manager w/ array
    //We are calling 1000x60fps Raycast per second
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) //layerMask == UI
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                tooltip.gameObject.transform.parent.gameObject.SetActive(true); // turn on tooltip (via parent BG)
                var props = GetComponent<MarkerDataContainer>().feature.Properties;
                // Render date and time text as bold and fontsize+5
                //TODO extract consts and scale based on screen size
                tooltip.text = $"<size=25><b>{props["address"]}\n{props["date"]}</b></size>\n\n<size=20>{props["description"]}</size>";
            }
        }
        // Scale the marker to fit nicely depending on how zoomed in you are
        this.transform.localScale = (new Vector3(50,50,50)) * (TopDownCameraController.scaleByZoomFactor*2);
    }
}
