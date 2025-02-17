using System;
using UnityEngine;

public class WasteCollectorGlass: MonoBehaviour
{
    
    public String[] acceptedTags = { "Glass" };
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("🟢 Collision detected with: " + other.gameObject.name); 
        foreach (string tag in acceptedTags)
        {
            if (other.CompareTag(tag))
            {
                Debug.Log("✅ Plastik genkendt! Destroying " + other.gameObject.name);
                ScoreManager.Instance.AddScore(scoreValue);
                Destroy(other.gameObject);
                return;
            }
        }
    }
  

  
}