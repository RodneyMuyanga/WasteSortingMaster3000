using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WastePool : MonoBehaviour
{
    public GameObject[] wastePrefabs;
    private Queue<GameObject> pooledWasteObjects = new Queue<GameObject>();
    public int poolSize = 10;

    private void Awake()
    {
        InitializePool();
    }

    private void OnEnable()
    {
        WasteBin.OnWasteSorted += ReturnToPool;
    }

    private void OnDisable()
    {
        WasteBin.OnWasteSorted -= ReturnToPool;
    }

    private void InitializePool()
    {
        if (wastePrefabs.Length == 0) return;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject wasteObject = Instantiate(GetRandomWastePrefab());
            wasteObject.SetActive(false);
            pooledWasteObjects.Enqueue(wasteObject);
        }
    }

    private GameObject GetRandomWastePrefab()
    {
        if (wastePrefabs.Length == 0) return null;
        int randomIndex = Random.Range(0, wastePrefabs.Length);
        return wastePrefabs[randomIndex];
    }

    public GameObject GetPooledWasteObject()
    {
        if (pooledWasteObjects.Count == 0) return null;

        GameObject wasteObject = pooledWasteObjects.Dequeue();
        wasteObject.SetActive(true);

        WasteItem wasteScript = wasteObject.GetComponent<WasteItem>();
        if (wasteScript != null)
        {
            wasteScript.ResetItem();
            wasteScript.SetSpeed(FindObjectOfType<WasteSpawner>().wasteSpeed);
        }

        return wasteObject;
    }

    public void ReturnToPool(GameObject wasteObject)
    {
        StartCoroutine(DelayedReturn(wasteObject));
    }

    private IEnumerator DelayedReturn(GameObject wasteObject)
    {
        yield return new WaitForSeconds(0.5f);
        wasteObject.SetActive(false);
        pooledWasteObjects.Enqueue(wasteObject);
    }
}
