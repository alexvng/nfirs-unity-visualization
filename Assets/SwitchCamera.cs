using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    private bool freeFly;
    [SerializeField] private GameObject camera1, camera2;

    private void Start()
    {
        freeFly = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            freeFly = !freeFly;
        }

        camera2.gameObject.SetActive(freeFly);
        camera1.gameObject.SetActive(!freeFly);
    }
}
