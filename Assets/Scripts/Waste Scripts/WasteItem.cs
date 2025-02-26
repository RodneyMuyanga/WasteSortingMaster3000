using UnityEngine;

public class WasteItem : MonoBehaviour
{
    private float speed = 5f; // Default speed

    void Update()
    {
        transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
    }

    //Method for updating speed
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // Update speed dynamically
    }
}