using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;
    public static List<GameObject> spawnedTiles = new List<GameObject>(); // Track spawned tiles

    void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
        spawnedTiles.Add(temp);
    }

    private void Start()
    {
        for (int i = 0; i < 15; i++) {
            SpawnTile();
        }
    }
    
    void Update()
    {
        if (spawnedTiles.Count > 20) // Keep a max of 20 tiles
        {
            GameObject oldTile = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            Destroy(oldTile);
        }
    }

}