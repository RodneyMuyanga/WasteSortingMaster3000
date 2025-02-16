using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefabs; // Liste over mad-prefabs
    public float spawnRate = 2f; // Tid mellem hver spawn
    public float xMin = -3f; // Juster for at matche din scene
    public float xMax = 3f;
    public float spawnHeight = 15f; // Højde hvor maden spawner

    void Start()
    {
        InvokeRepeating("SpawnFood", 1f, spawnRate); // Starter spawning
    }

    void SpawnFood()
    {
        // Vælg et tilfældigt mad-prefab
        int randomIndex = Random.Range(0, foodPrefabs.Length);
        GameObject foodToSpawn = foodPrefabs[randomIndex];

        // Generér en tilfældig position inden for baggrundens bredde
        float randomX = Random.Range(xMin, xMax);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, -1f);

        // Spawn mad-objektet
        GameObject newFood = Instantiate(foodToSpawn, spawnPosition, Quaternion.identity);
        
    }
}