using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectRaycast : MonoBehaviour
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
                var props = GetComponent<NFIRSView>().feature.Properties;
                tooltip.text = $"{props["address"]}\n{props["date"]}\n\n{props["description"]}";
            }
        }
        this.transform.localScale = (new Vector3(100,100,100)) * (CameraController.adjustBy*2);
    }
}
