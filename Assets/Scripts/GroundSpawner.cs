using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public float tileSpeed = 2f; // Made public
    private Vector3 nextSpawnPoint;
    public static List<GameObject> spawnedTiles = new List<GameObject>();
    public int maxTiles = 5;
    public float initialSpawnOffset = 5f;

    void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
        spawnedTiles.Add(temp);
    }

    private void Start()
    {
        nextSpawnPoint = new Vector3(0, 0, initialSpawnOffset);

        for (int i = 0; i < maxTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        foreach (GameObject tile in spawnedTiles)
        {
            tile.transform.position -= new Vector3(0, 0, tileSpeed * Time.deltaTime);
        }

        if (spawnedTiles.Count > 0 && spawnedTiles[0].transform.position.z < -10f)
        {
            GameObject oldestTile = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);

            oldestTile.transform.position = spawnedTiles[spawnedTiles.Count - 1].transform.GetChild(1).transform.position;
            spawnedTiles.Add(oldestTile);
        }
    }
}