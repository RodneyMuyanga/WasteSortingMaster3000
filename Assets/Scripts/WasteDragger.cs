using UnityEngine;

public class WasteDragger : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();

        // Stop moving when dragging starts
        WasteItem wasteScript = GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetMoving(false); // Stop the movement
        }
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Start moving again after dragging
        WasteItem wasteScript = GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetMoving(true); // Resume movement
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}