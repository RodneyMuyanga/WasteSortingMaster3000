using UnityEngine;

[RequireComponent(typeof(Collider))]  // Ensures we have a collider for mouse events
public class Drag3D : MonoBehaviour
{
    private Camera mainCam;
    private Plane dragPlane;
    private bool isDragging = false;
    private Vector3 offset;
    private float liftHeight = 3.0f; // Adjust this for higher lifting

    void Start()
    {
        mainCam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;

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
    }

}
