using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }
    private int wrongSorts = 0; // Tæller forkerte skraldespande
    private int maxWrongSorts = 3; // Game over ved 3 fejl

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Kald denne metode, når spilleren smider affald i den forkerte skraldespand
    public void AddWrongSort()
    {
        wrongSorts++;
        Debug.Log("Forkerte sorteringer: " + wrongSorts);

        if (wrongSorts >= maxWrongSorts)
        {
            // GameOver();
        }
    }

    // private void GameOver()
    // {
    //     Debug.Log("Game Over! Spilleren lavede 3 fejl.");
    //     ScoreManager.Instance.EndGame(); // Kalder EndGame() i ScoreManager
    // }
}