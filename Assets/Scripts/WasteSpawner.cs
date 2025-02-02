using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WasteSpawner : MonoBehaviour
{
    public GameObject[] wastePrefabs; // Assign waste prefabs
    public Transform spawnPoint; // Assign spawn position
    public float spawnAreaWidth = 12f; // Width of the spawn area
    public float minSpacingZ = 7f; // Minimum Z spacing between objects
    public float wasteSpeed = 5f; // Initial speed of waste movement
    public float speedIncreaseRate = 0.3f; // How much speed increases per second
    public bool isGameOver = false; // Track if the game is over

    private List<Vector3> lastSpawnPositions = new List<Vector3>(); // Track last spawn positions

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint is not assigned in the Inspector! Please assign a Transform.");
            return;
        }

        StartCoroutine(SpawnWasteWithDelay()); // Start natural, spaced spawning
        StartCoroutine(IncreaseWasteSpeedOverTime()); // Start increasing speed over time
    }

    IEnumerator SpawnWasteWithDelay()
    {
        while (!isGameOver)
        {
            SpawnWaste();
            
            // Wait randomly between 2 and 4 seconds before spawning the next item
            float randomDelay = Random.Range(2f, 4f);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    IEnumerator IncreaseWasteSpeedOverTime()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1f);
            wasteSpeed += speedIncreaseRate;
        }
    }

    void SpawnWaste()
    {
        if (wastePrefabs.Length == 0 || isGameOver) return;

        int randomIndex = Random.Range(0, wastePrefabs.Length);
        
        // Generate a completely random X position within the screen width
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        
        // Adjust Z position with some variation for a natural effect
        float randomZOffset = Random.Range(minSpacingZ * 0.8f, minSpacingZ * 1.2f);
        Vector3 spawnPosition = new Vector3(randomX, spawnPoint.position.y, spawnPoint.position.z + randomZOffset);

        GameObject waste = Instantiate(wastePrefabs[randomIndex], spawnPosition, wastePrefabs[randomIndex].transform.rotation);
        waste.transform.localScale = wastePrefabs[randomIndex].transform.localScale;

        WasteItem wasteScript = waste.GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetSpeed(wasteSpeed);
        }
    }
}
