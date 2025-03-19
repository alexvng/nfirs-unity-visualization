using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherHotkeys : MonoBehaviour
{
    private bool freeFly;
    [SerializeField] private GameObject camera1, camera2, tooltip, tiles1, tiles2;

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
