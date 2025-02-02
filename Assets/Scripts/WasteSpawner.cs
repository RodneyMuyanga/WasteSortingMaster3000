using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WasteSpawner : MonoBehaviour
{
    public GameObject[] wastePrefabs;
    public float spawnAreaWidth = 12f;
    public float minSpacingZ = 7f;
    public float wasteSpeed = 5f;
    public float speedIncreaseRate = 0.3f;
    public bool isGameOver = false;

    private List<Vector3> lastSpawnPositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(SpawnWasteWithDelay());
        StartCoroutine(IncreaseWasteSpeedOverTime());
    }

    IEnumerator SpawnWasteWithDelay()
    {
        while (!isGameOver)
        {
            SpawnWaste();
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

        // Get a random ground tile for spawning waste
        if (GroundSpawner.spawnedTiles.Count == 0) return; // Avoid errors

        GameObject randomTile = GroundSpawner.spawnedTiles[Random.Range(0, GroundSpawner.spawnedTiles.Count)];
        Vector3 tilePosition = randomTile.transform.position;

        // Spawn waste slightly above the ground tile
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        Vector3 spawnPosition = new Vector3(randomX, tilePosition.y + 1f, tilePosition.z);

        int randomIndex = Random.Range(0, wastePrefabs.Length);
        GameObject waste = Instantiate(wastePrefabs[randomIndex], spawnPosition, Quaternion.identity);
        
        WasteItem wasteScript = waste.GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.SetSpeed(wasteSpeed);
        }
    }
}