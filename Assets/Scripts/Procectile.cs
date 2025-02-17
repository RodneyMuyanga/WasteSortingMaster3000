using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Awake()
    {
        Destroy(gameObject, 3);
        Rigidbody rb = GetComponent<Rigidbody>();

        // Brug transform.forward i stedet for Vector3.forward
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food")) // Sørg for at pizzaen har tag "Food"
        {
            ScoreManager.Instance.AddScore(10); // Tilføj 10 point
            Destroy(other.gameObject); // Slet pizzaen
            Destroy(gameObject); // Slet ormen
        }
    }
}