using UnityEngine;

public class WasteItem : MonoBehaviour
{
    private float speed = 5f;
    private bool isMoving = true;
    private WastePool wastePool;

    void Start()
    {
        wastePool = FindObjectOfType<WastePool>();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            if (IsOutOfBounds())
            {
                ReturnToPool();
            }
        }
    }

    // Sets the speed of the waste item
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Starts or stops the waste item from moving
    public void SetMoving(bool shouldMove)
    {
        isMoving = shouldMove;
    }

    // Checks if the waste item is out of bounds
    private bool IsOutOfBounds()
    {
        if (Camera.main == null) return false;
        float cameraZ = Camera.main.transform.position.z;
        return transform.position.z < cameraZ - 20f;
    }

    // Returns the waste item to the pool
    private void ReturnToPool()
    {
        if (wastePool != null)
        {
            wastePool.ReturnToPool(gameObject);
        }
    }
}
