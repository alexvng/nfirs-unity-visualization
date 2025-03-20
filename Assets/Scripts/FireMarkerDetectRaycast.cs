using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireMarkerDetectRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private TMP_Text tooltip;

    private void Start()
    {
        //tooltip = FindAnyObjectByType<TMP_Text>();
        tooltip = GameObject.FindWithTag("tooltip_text").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                tooltip.gameObject.transform.parent.gameObject.SetActive(true);
                var props = GetComponent<MarkerDataContainer>().feature.Properties;
                tooltip.text = $"<size=25><b>{props["address"]}\n{props["date"]}</b></size>\n\n<size=20>{props["description"]}</size>";
            }
        }
        this.transform.localScale = (new Vector3(50,50,50)) * (TopDownCameraController.scaleMovementByZoomFactor*2);
    }
}
