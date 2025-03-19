using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of camera movement
    public float zoomSpeed = 10f;    // Speed of zoom (dolly effect)

    private Vector3 dragOrigin;      // Starting position of the camera on drag
    private bool isDragging = false; // Flag to check if the mouse is being dragged

    public static float adjustBy = 0.05f;

    private void OnDisable()
    {
        adjustBy = 0.1f;
    }

    void Update()
    {
        HandleMouseDrag();
        HandleMouseZoom();
        float currentZoomLevel = Mathf.Abs(transform.position.y - -14500f);
        adjustBy = Mathf.Clamp((currentZoomLevel) / (14500), 0.05f, 1f);
    }

    // Handles the click-and-drag movement
    private void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))  // Left click to start drag
        {
            isDragging = true;
            dragOrigin = new Vector3();
            dragOrigin.x = Mouse.current.position.ReadValue().x;
            dragOrigin.z = Mouse.current.position.ReadValue().y;
            dragOrigin.y = 0; // Keep the camera in the same Z-plane
        }

        if (Input.GetMouseButtonUp(0))  // Stop dragging
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = new Vector3();
            currentMousePosition.x = Mouse.current.position.ReadValue().x;
            currentMousePosition.z = Mouse.current.position.ReadValue().y;
            currentMousePosition.y = 0;  // Lock the camera to the same Z-plane
            Vector3 delta = dragOrigin - currentMousePosition;

            // Move the camera based on mouse drag
            transform.position += delta * moveSpeed * Time.deltaTime * adjustBy;
            dragOrigin = currentMousePosition;
        }
    }

    // Handles zooming in and out with the scroll wheel (dolly zoom)
    private void HandleMouseZoom()
    {
        float scrollInput = Mouse.current.scroll.y.ReadValue();
        if (scrollInput != 0f)
        {
            float zoomAmount = scrollInput * zoomSpeed;

            float newY = transform.position.y - (zoomAmount * adjustBy); // Zoom effect along the Z-axis
            newY = Mathf.Clamp(newY, -14600f, 0f);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
