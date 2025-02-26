using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WasteCollectorFood : MonoBehaviour
{
    public String[] acceptedTags = { "Food" };
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