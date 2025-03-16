using UnityEngine;
using System;
using System.Collections;

public class WasteBin : MonoBehaviour
{
    public string correctTag;
    private ScoreManager scoreManager;

    public static event Action<GameObject> OnWasteSorted;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(other.gameObject.tag))
        {
            Debug.LogError("Tag is null or empty for: " + other.gameObject.name);
            return;
        }

        WasteItem wasteItem = other.GetComponent<WasteItem>();
        if (wasteItem != null)
        {
            wasteItem.SetMoving(false);
            StartCoroutine(DropIntoBin(other.transform));
        }

        if (other.CompareTag(correctTag))
        {
            scoreManager.AddScore(1);
        }
        else
        {
            scoreManager.AddScore(-1);
        }
    }

    private IEnumerator DropIntoBin(Transform trash)
    {
        Vector3 targetPosition = transform.position + Vector3.down * 0.7f;
        float dropSpeed = 2f;
        float timer = 2f;

        while (Vector3.Distance(trash.position, targetPosition) > 0.05f && timer > 0)
        {
            trash.position = Vector3.MoveTowards(trash.position, targetPosition, dropSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
            yield return null;
        }

        // Notify pooling even if the object doesn't reach the bottom
        OnWasteSorted?.Invoke(trash.gameObject);
    }
}
