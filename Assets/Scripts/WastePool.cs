using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WastePool : MonoBehaviour
{
    public GameObject[] wastePrefabs;
    private Queue<GameObject> pooledWasteObjects = new Queue<GameObject>();
    public int poolSize = 10;

    void Awake()
    {
        InitializePool();
    }

    // Initializes the pool with a set number of waste objects
    void InitializePool()
    {
        if (wastePrefabs.Length == 0) return;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject wasteObject = Instantiate(GetRandomWastePrefab());
            wasteObject.SetActive(false);
            pooledWasteObjects.Enqueue(wasteObject);
        }
    }

    // Returns a random waste prefab
    public GameObject GetRandomWastePrefab()
    {
        if (wastePrefabs.Length == 0) return null;
        int randomIndex = Random.Range(0, wastePrefabs.Length);
        return wastePrefabs[randomIndex];
    }

    // Gets a pooled waste object
    public GameObject GetPooledWasteObject()
    {
        if (pooledWasteObjects.Count == 0) return null;
        GameObject wasteObject = pooledWasteObjects.Dequeue();
        if (wasteObject.activeInHierarchy)
        {
            StartCoroutine(WaitBeforeReuse(wasteObject));
            return null;
        }
        wasteObject.SetActive(true);
        pooledWasteObjects.Enqueue(wasteObject);
        return wasteObject;
    }

    // Returns a waste object to the pool
    public void ReturnToPool(GameObject wasteObject)
    {
        StartCoroutine(DelayedReturn(wasteObject));
    }

    // Delays the return of an object to the pool
    private IEnumerator DelayedReturn(GameObject wasteObject)
    {
        yield return new WaitForSeconds(0.2f);
        wasteObject.SetActive(false);
    }

    // Delays the reuse of a waste object
    private IEnumerator WaitBeforeReuse(GameObject wasteObject)
    {
        yield return new WaitForSeconds(1f);
        pooledWasteObjects.Enqueue(wasteObject);
    }
}
