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
    }

    // Starts spawning waste after an initial delay
    IEnumerator StartSpawningWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnWasteWithDelay());
        StartCoroutine(IncreaseWasteSpeedOverTime());
        StartCoroutine(DecreaseSpawnDelayOverTime());
    }

    // Spawns waste at random intervals
    IEnumerator SpawnWasteWithDelay()
    {
        while (!isGameOver)
        {
            SpawnWaste();
            float randomDelay = Random.Range(minSpawnDelay, currentMaxDelay);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    // Increases the waste speed over time
    IEnumerator IncreaseWasteSpeedOverTime()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1f);
            wasteSpeed += speedIncreaseRate;
        }
    }

    // Decreases the spawn delay over time
    IEnumerator DecreaseSpawnDelayOverTime()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(5f);
            currentMaxDelay = Mathf.Max(minSpawnDelay, currentMaxDelay - spawnDelayDecreaseRate);
        }
    }

    // Spawns a waste object at a random position on the latest tile
    void SpawnWaste()
    {
        if (wastePool == null) return;

        if (GroundSpawner.spawnedTiles.Count == 0) return;

        GameObject latestTile = GroundSpawner.spawnedTiles[GroundSpawner.spawnedTiles.Count - 1];
        Vector3 tilePosition = latestTile.transform.position;

        Renderer tileRenderer = latestTile.GetComponent<Renderer>();
        float tileWidth = tileRenderer != null ? tileRenderer.bounds.size.x : 12f;

        Vector3 startOfTile = latestTile.transform.GetChild(0).position;

        float randomX = Random.Range(-tileWidth / 2, tileWidth / 2);
        Vector3 spawnPosition = new Vector3(tilePosition.x + randomX, tilePosition.y + 1f, startOfTile.z);

        GameObject waste = wastePool.GetPooledWasteObject();
        if (waste == null) return;

        waste.transform.position = spawnPosition;
        WasteItem wasteScript = waste.GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetSpeed(wasteSpeed);
        }
    }
}
