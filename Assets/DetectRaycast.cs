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
        tooltip = FindAnyObjectByType<TMP_Text>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                tooltip.text = GetComponent<NFIRSView>().data._addr;
                tooltip.text += "\n";
                tooltip.text += GetComponent<NFIRSView>().data._desc;
            }
        }
    }
}
