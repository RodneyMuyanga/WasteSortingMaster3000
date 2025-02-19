using UnityEngine;

public class WasteBin : MonoBehaviour
{
    public string correctTag; // Set in Unity Editor for each bin
    private ScoreManager scoreManager;

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
        
        Debug.Log("Entering trigger with object: " + other.gameObject.name + " (Tag: " + other.gameObject.tag + ")");
        
        if (other.CompareTag(correctTag))
        {
            Debug.Log("Correctly Sorted!");
            scoreManager.AddScore(1);
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("Wrong Bin!");
            scoreManager.AddScore(-1);
        }
    }
}