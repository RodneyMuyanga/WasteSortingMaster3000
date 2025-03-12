using UnityEngine; // Importing Unity's core library

public class WasteDragger : MonoBehaviour // Attach this script to a GameObject to enable dragging
{
    private bool isDragging = false; // Keeps track of whether the object is being dragged
    private Vector3 offset; // Stores the distance between the mouse click and the object's position

    // Called when the user clicks on the object
    void OnMouseDown()
    {
        isDragging = true; // Set dragging state to true
        offset = transform.position - GetMouseWorldPos(); // Calculate the offset between the object and mouse position
    }

    // Called continuously while the object is being dragged
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset; // Move the object with the mouse while keeping the offset
    }

    // Called when the user releases the mouse button
    void OnMouseUp()
    {
        isDragging = false; // Set dragging state to false
    }

    // Helper function to get the mouse position in world space
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition; // Get the current mouse position in screen coordinates
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Maintain object's depth in world space
        return Camera.main.ScreenToWorldPoint(mousePos); // Convert the screen position to a world position
    }
}