using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private AudioSource shootSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);

        if (shootSound != null)
        {
            shootSound.Play();
        }
        else
        {
            {
                Debug.LogWarning("No shoot sound assigned to the Shooting script");
            }
        }
    }
}
