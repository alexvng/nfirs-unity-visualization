using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/// <summary>
/// Google Maps-style navigation with mouse drag and scroll wheel
/// <ai>Partially written by Copilot AI</ai>
/// </summary>
public class TopDownCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float zoomSpeed = 10f;

    private Vector3 dragOrigin;
    private bool isDragging = false;

    public static float scaleByZoomFactor = 0.05f;

    private void OnDisable()
    {
        scaleByZoomFactor = 0.1f;
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            HandleMouseDrag();
        }
        HandleMouseZoom();
        float currentZoomLevel = Mathf.Abs(transform.position.y - -14500f);
        scaleByZoomFactor = Mathf.Clamp((currentZoomLevel) / (14500), 0.05f, 1f);
    }

    private void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragOrigin = new Vector3();
            dragOrigin.x = Mouse.current.position.ReadValue().x;
            dragOrigin.z = Mouse.current.position.ReadValue().y;
            dragOrigin.y = 0;
        }

        if (!Input.GetMouseButton(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = new Vector3();
            currentMousePosition.x = Mouse.current.position.ReadValue().x;
            currentMousePosition.z = Mouse.current.position.ReadValue().y;
            currentMousePosition.y = 0;
            Vector3 delta = dragOrigin - currentMousePosition;

            transform.position += delta * moveSpeed * (Time.deltaTime / 2) * scaleByZoomFactor;
            dragOrigin = currentMousePosition;
        }
    }

    private void HandleMouseZoom()
    {
        float scrollInput = Mouse.current.scroll.y.ReadValue();
        if (scrollInput != 0f)
        {
            float zoomAmount = scrollInput * zoomSpeed;

            float newY = transform.position.y - (zoomAmount * scaleByZoomFactor);
            newY = Mathf.Clamp(newY, -14600f, 0f);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
