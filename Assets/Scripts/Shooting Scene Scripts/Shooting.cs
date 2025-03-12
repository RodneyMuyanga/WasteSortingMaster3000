using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        }
    }
}
