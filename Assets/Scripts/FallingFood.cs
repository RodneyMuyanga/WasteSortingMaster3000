using UnityEngine;

public class FallingFood : MonoBehaviour
{
    public float fallSpeed = 1f;
    void Update()
    {
        // Sørg for, at pizzaen altid har den ønskede rotation
        transform.rotation = Quaternion.Euler(95, 0, 173);
        transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
    }
}
