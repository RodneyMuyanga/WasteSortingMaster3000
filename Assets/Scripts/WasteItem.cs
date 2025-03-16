using UnityEngine;

public class WasteItem : MonoBehaviour
{
    private float speed = 5f; // Default speed
    private float originalSpeed;
    private bool isFrozen = false;

    void start()
    {
        originalSpeed = speed;
    }

    void Update()
    {
        if (!isFrozen)
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
    }

    //Method for updating speed
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // Update speed dynamically
        originalSpeed = newSpeed;
    }

    public void Freeze()
    {
        isFrozen = true;
        speed = 0f;
    }

    public void Unfreeze()
    {
        isFrozen = false;
        speed = originalSpeed;
    }
}