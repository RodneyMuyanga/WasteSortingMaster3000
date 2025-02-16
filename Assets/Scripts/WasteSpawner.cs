using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Spawns waste objects at random intervals and increases their speed over time
public class WasteSpawner : MonoBehaviour
{
    public GameObject[] wastePrefabs; // Array of waste object prefabs
    public float spawnAreaWidth = 12f; // Width of the spawn area
    public float wasteSpeed = 5f; // Initial speed of waste objects
    public float speedIncreaseRate = 0.3f; // Rate at which waste speed increases
    public bool isGameOver = false; // Flag to check if the game is over

    private float minSpawnDelay = 0.5f; // Minimum delay (fastest spawning)
    private float maxSpawnDelay = 4f; // Maximum initial delay
    private float spawnDelayDecreaseRate = 0.05f; // Rate at which spawn delay decreases
    private float currentMaxDelay; // Tracks the current max delay

    void Start()
    {
        currentMaxDelay = maxSpawnDelay; // Set initial spawn delay range
        StartCoroutine(SpawnWasteWithDelay()); // Starts the coroutine to spawn waste at intervals
        StartCoroutine(IncreaseWasteSpeedOverTime()); // Starts the coroutine to gradually increase waste speed
        StartCoroutine(DecreaseSpawnDelayOverTime()); // Start reducing spawn delay
    }

    IEnumerator SpawnWasteWithDelay()
    {
        while (!isGameOver) // Keeps spawning waste until the game is over
        {
            SpawnWaste(); // Calls function to spawn a waste object
            float randomDelay = Random.Range(minSpawnDelay, currentMaxDelay); // Dynamic delay
            yield return new WaitForSeconds(randomDelay); // Waits before spawning the next waste object
        }
    }

    IEnumerator IncreaseWasteSpeedOverTime()
    {
        while (!isGameOver) // Increases speed continuously until the game ends
        {
            yield return new WaitForSeconds(1f); // Waits for 1 second before increasing speed
            wasteSpeed += speedIncreaseRate; // Increases the speed of waste objects
        }
    }

    IEnumerator DecreaseSpawnDelayOverTime()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(5f); // Every 5 seconds, reduce spawn delay
            currentMaxDelay = Mathf.Max(minSpawnDelay, currentMaxDelay - spawnDelayDecreaseRate);
            Debug.Log("New spawn delay range: " + minSpawnDelay + " to " + currentMaxDelay);
        }
    }

    void SpawnWaste()
    {
        if (wastePrefabs.Length == 0 || isGameOver) return; // Stops if no waste prefabs or game is over
        if (GroundSpawner.spawnedTiles.Count == 0) return; // Stops if no ground tiles exist

        // Get the latest spawned ground tile
        GameObject latestTile = GroundSpawner.spawnedTiles[GroundSpawner.spawnedTiles.Count - 1];
        Vector3 tilePosition = latestTile.transform.position;
        
        // Get the tile's width dynamically
        Renderer tileRenderer = latestTile.GetComponent<Renderer>();
        float tileWidth = tileRenderer != null ? tileRenderer.bounds.size.x : 12f; // Default to 12 if no renderer

        // Adjust position to be on top of the tile
        Vector3 startOfTile = latestTile.transform.GetChild(0).position; // Assuming first child is the front

        // Pick a random X position within the tile's width
        float randomX = Random.Range(-tileWidth / 2, tileWidth / 2);
        Vector3 spawnPosition = new Vector3(tilePosition.x + randomX, tilePosition.y + 1f, startOfTile.z); // On top of tile

        // Choose a random waste item to spawn
        int randomIndex = Random.Range(0, wastePrefabs.Length);
        GameObject waste = Instantiate(wastePrefabs[randomIndex], spawnPosition, Quaternion.identity); // Spawn waste

        WasteItem wasteScript = waste.GetComponent<WasteItem>(); 
        if (wasteScript != null)
        {
            wasteScript.SetSpeed(wasteSpeed); // Set waste speed
        }
    }
}
