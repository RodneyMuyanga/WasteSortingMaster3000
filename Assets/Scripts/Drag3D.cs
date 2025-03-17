using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]  // Ensures we have a collider for mouse events
public class Drag3D : MonoBehaviour
{
    private Camera mainCam;
    private Plane dragPlane;
    private bool isDragging = false;
    private Vector3 offset;
    private float liftHeight = 4.5f; // heigh when lifting
    private float targetYPosition = 0f; // Target Y position for the object after being dropped (e.g., the conveyor height)
    private float smoothTime = 0.2f; // Time for the Y position to smoothly adjust

    private Vector3 velocity = Vector3.zero;  // For smooth damping

    void Start()
    {
        mainCam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;

        // Disable gravity while holding
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero; // Stop any movement
        }

        // Adjust the drag plane to be higher when picking up
        dragPlane = new Plane(Vector3.up, transform.position + Vector3.up * liftHeight);

        // Create a ray from the camera through the mouse position
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (dragPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            offset = (transform.position + Vector3.up * liftHeight) - hitPoint;
        }

        // Instantly move the trash higher when picked up
        transform.position += Vector3.up * liftHeight;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (dragPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            transform.position = hitPoint + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.linearVelocity = Vector3.zero; // Stop any movement when dropped
        }

        // Start smooth transition to the target Y position (e.g., conveyor or garbage can height)
        StartCoroutine(SmoothDrop());
    }

    IEnumerator SmoothDrop()
    {
        float timeElapsed = 0f;
        Vector3 currentPosition = transform.position;

        // Keep X and Z the same, only interpolate the Y position
        Vector3 targetPosition = new Vector3(currentPosition.x, targetYPosition, currentPosition.z);

        while (timeElapsed < smoothTime)
        {
            // Interpolate the Y position smoothly over time
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Once it's done, ensure the final position is exact
        transform.position = targetPosition;
    }
}
