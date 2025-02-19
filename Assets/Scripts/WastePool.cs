using UnityEngine;
using System.Collections.Generic;

public class WastePool : MonoBehaviour
{
    public GameObject[] wastePrefabs; // Array to hold different waste prefabs
    private List<GameObject> pooledWasteObjects; // List to store pooled objects
    public int poolSize = 10; // Number of objects to pool initially

    void Awake()
    {
        pooledWasteObjects = new List<GameObject>();
        InitializePool(); // Initialize the pool of objects
    }

    // Initializes the pool with a predefined number of objects
    void InitializePool()
    {
        if (wastePrefabs.Length == 0)
        {
            Debug.LogError("No wastePrefabs assigned!");
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject wasteObject = Instantiate(GetRandomWastePrefab()); // Instantiate a random prefab
            wasteObject.SetActive(false); // Ensure the object is inactive initially
            pooledWasteObjects.Add(wasteObject); // Add it to the pool
        }

        Debug.Log("Pool initialized with " + pooledWasteObjects.Count + " objects.");
    }

    // Gets a random prefab from the wastePrefabs array
    public GameObject GetRandomWastePrefab()
    {
        if (wastePrefabs.Length == 0)
        {
            Debug.LogError("No wastePrefabs assigned!");
            return null;
        }

        int randomIndex = Random.Range(0, wastePrefabs.Length);
        Debug.Log("Fetching prefab: " + wastePrefabs[randomIndex].name); // Debugging the selected prefab
        return wastePrefabs[randomIndex];
    }

    // Returns a pooled object if available or creates a new one if necessary
    public GameObject GetPooledWasteObject()
    {
        // Try to find an inactive object in the pool
        for (int i = 0; i < pooledWasteObjects.Count; i++)
        {
            if (!pooledWasteObjects[i].activeInHierarchy)
            {
                pooledWasteObjects[i].SetActive(true); // Reactivate the object
                Debug.Log("Returning pooled object: " + pooledWasteObjects[i].name); // Debugging log
                return pooledWasteObjects[i];
            }
        }

        // If no pooled object is inactive, create a new one
        GameObject newWasteObject = Instantiate(GetRandomWastePrefab());
        pooledWasteObjects.Add(newWasteObject);
        Debug.Log("Creating new pooled object: " + newWasteObject.name); // Debugging log
        return newWasteObject;
    }

    // Method to return the pooled object back to the pool
    public void ReturnToPool(GameObject wasteObject)
    {
        wasteObject.SetActive(false); // Deactivate the object before returning it to the pool
        Debug.Log("Returning object to pool: " + wasteObject.name); // Debugging log
    }
}
