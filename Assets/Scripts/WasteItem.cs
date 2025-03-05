using UnityEngine;

public class WasteItem : MonoBehaviour
{
    private float speed = 5f;
    private bool isMoving = true;
    private WastePool wastePool;
    private Rigidbody rb;

    void Start()
    {
        wastePool = FindObjectOfType<WastePool>();
        rb = GetComponent<Rigidbody>();
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

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetMoving(bool shouldMove)
    {
        isMoving = shouldMove;
    }

    private bool IsOutOfBounds()
    {
        if (Camera.main == null) return false;
        float cameraZ = Camera.main.transform.position.z;
        return transform.position.z < cameraZ - 15f; // Increased buffer
    }

    public void ResetItem()
    {
        isMoving = true;
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
    }

    private void ReturnToPool()
    {
        if (wastePool != null)
        {
            wastePool.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GarbageCan"))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            isMoving = false;
        }
    }
}