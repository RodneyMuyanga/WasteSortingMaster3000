using UnityEngine;
using UnityEngine.SceneManagement; // Kun nødvendigt hvis du vil reloade scenen

public class MissedFoodCounter : MonoBehaviour
{
    public int missedCount = 0; // Hvor mange mad-objekter er missede
    public int maxMissed = 10; // Antal missede mad før Game Over

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food")) // Sørg for at alle mad-prefabs har tagget "Food"
        {
            missedCount++;
            Destroy(other.gameObject); // Slet maden

            if (missedCount >= maxMissed)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!"); // Vis i konsollen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scenen
    }
}