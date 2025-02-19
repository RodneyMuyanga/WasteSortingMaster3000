using UnityEngine;
using System.Collections;

public class WasteSpawner : MonoBehaviour
{
    public GameObject[] wastePrefabs;  // Array to hold different waste prefabs
    public float spawnAreaWidth = 12f;
    public float wasteSpeed = 5f;
    public float speedIncreaseRate = 0.3f;
    public bool isGameOver = false;

    private float minSpawnDelay = 0.5f;
    private float maxSpawnDelay = 4f;
    private float spawnDelayDecreaseRate = 0.05f;
    private float currentMaxDelay;
    private WastePool wastePool;
    private GroundSpawner groundSpawner;

    void Start()
    {
        currentMaxDelay = maxSpawnDelay;
        wastePool = FindObjectOfType<WastePool>();
        groundSpawner = FindObjectOfType<GroundSpawner>();
        StartCoroutine(SpawnWasteWithDelay());
        StartCoroutine(IncreaseWasteSpeedOverTime());
        StartCoroutine(DecreaseSpawnDelayOverTime());
    }

    IEnumerator SpawnWasteWithDelay()
    {
        while (!isGameOver)
        {
            SpawnWaste();
            float randomDelay = Random.Range(minSpawnDelay, currentMaxDelay);
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

    IEnumerator DecreaseSpawnDelayOverTime()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(5f);
            currentMaxDelay = Mathf.Max(minSpawnDelay, currentMaxDelay - spawnDelayDecreaseRate);
            Debug.Log("New spawn delay range: " + minSpawnDelay + " to " + currentMaxDelay); // Debug log
        }
    }

    void SpawnWaste()
    {
        if (wastePool == null)
        {
            Debug.LogError("WastePool not assigned!");
            return;
        }

        if (GroundSpawner.spawnedTiles.Count == 0) return;

        // Get the latest spawned ground tile
        GameObject latestTile = GroundSpawner.spawnedTiles[GroundSpawner.spawnedTiles.Count - 1];
        Vector3 tilePosition = latestTile.transform.position;

        // Get the tile's width dynamically
        Renderer tileRenderer = latestTile.GetComponent<Renderer>();
        float tileWidth = tileRenderer != null ? tileRenderer.bounds.size.x : 12f; // Default to 12 if no renderer

        // Adjust spawn position to be on top of the tile
        Vector3 startOfTile = latestTile.transform.GetChild(0).position; // Assuming first child is the front

        // Pick a random X position within the tile's width
        float randomX = Random.Range(-tileWidth / 2, tileWidth / 2);
        Vector3 spawnPosition = new Vector3(tilePosition.x + randomX, tilePosition.y + 1f, startOfTile.z); // On top of tile

        // Fetch a pooled waste object
        GameObject waste = wastePool.GetPooledWasteObject();
        if (waste == null)
        {
            Debug.LogError("No pooled waste objects available!");
            return;
        }

        // Reset the position of the pooled object
        waste.transform.position = spawnPosition;
        Debug.Log("Spawned waste at: " + spawnPosition); // Debugging spawn position

        // Set the speed of the waste object
        WasteItem wasteScript = waste.GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetSpeed(wasteSpeed);
        }

        // Return the object to the pool when it is destroyed or goes out of bounds (example for cleanup)
        // Example: waste will return to pool after 5 seconds (or based on your game's mechanics)
        StartCoroutine(ReturnWasteToPool(waste, 5f)); // Returns after 5 seconds
    }

    // Coroutine to handle returning the waste object to the pool
    IEnumerator ReturnWasteToPool(GameObject waste, float delay)
    {
        yield return new WaitForSeconds(delay);
        wastePool.ReturnToPool(waste); // Return to the pool after a delay
    }
}
