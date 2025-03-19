using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }
}
