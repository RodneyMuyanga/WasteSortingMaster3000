using System;
using UnityEngine;

public class WasteCollectorElectronics : MonoBehaviour
{
    public String[] acceptedTags = { "Eletronic" };
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