using UnityEngine;
using System.Collections;

public class WasteSpawner : MonoBehaviour
{
    public float spawnAreaWidth = 12f;
    public float wasteSpeed = 5f;
    public float speedIncreaseRate = 0.3f;
    public bool isGameOver = false;

    private float minSpawnDelay = 0.5f;
    private float maxSpawnDelay = 4f;
    private float spawnDelayDecreaseRate = 0.05f;
    private float currentMaxDelay;
    private WastePool wastePool;

    void Start()
    {
        currentMaxDelay = maxSpawnDelay;
        wastePool = FindObjectOfType<WastePool>();
        float initialDelay = Random.Range(0.5f, 2f);
        StartCoroutine(StartSpawningWithDelay(initialDelay));
        StartCoroutine(IncreaseWasteSpeedOverTime());
        StartCoroutine(DecreaseSpawnDelayOverTime());
    }

    IEnumerator StartSpawningWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnWasteWithDelay());
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
        }
    }

    void SpawnWaste()
{
    if (wastePool == null) return;
    if (GroundSpawner.spawnedTiles.Count == 0) return;

    // Get the last spawned ground tile
    GameObject latestTile = GroundSpawner.spawnedTiles[GroundSpawner.spawnedTiles.Count - 1];
    Vector3 tilePosition = latestTile.transform.position;

    // Get the width of the tile for random X position
    Renderer tileRenderer = latestTile.GetComponent<Renderer>();
    float tileWidth = tileRenderer != null ? tileRenderer.bounds.size.x : 12f;

    // Find the front (start) of the tile
    Vector3 startOfTile = latestTile.transform.GetChild(0).position;

    // Define a safe margin from the edges where the waste can spawn
    float safeMargin = 2f; // Adjust this value to determine how far from the edge you want to spawn

    // Ensure random X position stays within the safe area
    float randomX = Random.Range(-tileWidth / 2 + safeMargin, tileWidth / 2 - safeMargin);

    // Set spawn position
    Vector3 spawnPosition = new Vector3(tilePosition.x + randomX, tilePosition.y + 1f, startOfTile.z);

    // Get an object from the pool
    GameObject waste = wastePool.GetPooledWasteObject();
    if (waste == null) return;

    // Set position and activate
    waste.transform.position = spawnPosition;
    waste.SetActive(true);

    // Ensure it moves in the correct direction
    WasteItem wasteScript = waste.GetComponent<WasteItem>();
    if (wasteScript != null)
    {
        wasteScript.SetSpeed(wasteSpeed);
    }
}

}
