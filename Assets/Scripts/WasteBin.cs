using UnityEngine;

//This is also just an example script, it's not attacked to anything yet
public class WasteBin : MonoBehaviour
{
    public string correctTag; // 
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            Debug.Log("Correctly Sorted!");
            scoreManager.UpdateScore(1);
            Destroy(other.gameObject); 
        }
        else
        {
            Debug.Log("Wrong Bin!");
            scoreManager.UpdateScore(-1);
        }
    }
}