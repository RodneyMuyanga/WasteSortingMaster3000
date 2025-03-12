using System;
using UnityEngine;

public class WasteCollectorGlass : MonoBehaviour
{
    public String[] acceptedTags = { "Glass" };
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in acceptedTags)
        {
            if (other.CompareTag(tag))
            {
                ScoreManager.Instance.AddScore(scoreValue);
                Destroy(other.gameObject);
                return;
            }
        }
    }
}