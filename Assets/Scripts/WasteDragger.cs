using UnityEngine;

public class WasteDragger : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private float initialZ;
    private float smoothSpeed = 10f;

    void OnMouseDown()
    {
        isDragging = true;
        initialZ = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos(initialZ);
        WasteItem wasteScript = GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetMoving(false);
        }
    }

    void OnMouseDrag()
    {
        Vector3 newPos = GetMouseWorldPos(initialZ) + offset;
        newPos.z = Mathf.Lerp(transform.position.z, GetZPositionFromScreenDrag(), Time.deltaTime * smoothSpeed);
        transform.position = newPos;
    }

    void OnMouseUp()
    {
        isDragging = false;
        WasteItem wasteScript = GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetMoving(true);
        }
    }

    // Converts mouse position to world space based on Z depth
    Vector3 GetMouseWorldPos(float zDepth)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDepth;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    // Get the Z position based on the drag distance
    float GetZPositionFromScreenDrag()
    {
        float screenHeight = Screen.height;
        float dragY = Input.mousePosition.y / screenHeight;
        float minZ = Camera.main.transform.position.z + 2f;
        float maxZ = Camera.main.transform.position.z + 20f;
        return Mathf.Lerp(minZ, maxZ, dragY);
    }
}