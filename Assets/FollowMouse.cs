using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    // Update is called once per frame
    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            this.gameObject.SetActive(false);
            print(hit.collider.gameObject.name);
        }
    }
}
