using UnityEngine;

public class WasteItem : MonoBehaviour
{
    private float speed = 5f;
    private bool isMoving = true; // Default to moving

    void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Add this method to stop/start the movement
    public void SetMoving(bool shouldMove)
    {
        isMoving = shouldMove;
    }
}