using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Update position of tooltip to always match mouse
/// </summary>
public class TooltipFollowMouse : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, Mathf.Infinity, layerMask)) // hide if raycast fails
        {
            this.gameObject.SetActive(false);
        }
    }
}
