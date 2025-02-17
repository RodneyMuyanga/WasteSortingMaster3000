using UnityEngine;

public class GunController : MonoBehaviour
{
    public float moveSpeed = 5f; // Bevægelse til venstre/højre
    public float rotationSpeed = 30f; // Hvor hurtigt pistolen kan rotere op/ned
    public float minRotation = -30f; // Minimum vinkel (nedad)
    public float maxRotation = 30f; // Maksimum vinkel (opad)

    private float currentRotation = 0f; // Holder styr på pistolens vinkel

    void Update()
    {
        float move = 0;
        float rotation = 0;

        // Bevæg pistolen sidelæns
        if (Input.GetKey(KeyCode.LeftArrow)) move = -moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) move = moveSpeed * Time.deltaTime;

        // Hæv/sænk pistolens sigtevinkel (roter om X-aksen)
        if (Input.GetKey(KeyCode.UpArrow)) rotation = -rotationSpeed * Time.deltaTime; // Negativ for at hæve
        if (Input.GetKey(KeyCode.DownArrow)) rotation = rotationSpeed * Time.deltaTime; // Positiv for at sænke

        // Opdater pistolens position
        transform.position += new Vector3(move, 0, 0);

        // Opdater rotation (brug X-aksen for op/ned i 3D)
        currentRotation = Mathf.Clamp(currentRotation + rotation, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(currentRotation, 0, 0); // Roter om X-aksen
    }
}