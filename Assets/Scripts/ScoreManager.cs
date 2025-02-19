using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This is just an example, i didn't attack the scrip to unity
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;

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
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
       UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    
    // 🔹 NY METODE: Gemmer highscore og skifter til highscore-scenen
    public void EndGame()
    {
        int finalScore = score; // Henter spillerens score
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");

        // Indsætter score i highscore-listen
        HighscoreManager.highscoreList.Add((playerName, finalScore));

        // Sorterer listen og gemmer kun de 10 bedste
        HighscoreManager.highscoreList.Sort((a, b) => b.score.CompareTo(a.score));
        if (HighscoreManager.highscoreList.Count > 10)
        {
            HighscoreManager.highscoreList.RemoveAt(10);
        }

        // Gemmer highscores i PlayerPrefs
        for (int i = 0; i < HighscoreManager.highscoreList.Count; i++)
        {
            PlayerPrefs.SetString("Highscore_Name_" + i, HighscoreManager.highscoreList[i].playerName);
            PlayerPrefs.SetInt("Highscore_Score_" + i, HighscoreManager.highscoreList[i].score);
        }
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("HighscoreScene");
    }
    
    
    /*
    
    //Metode til at kalde EndGame
    
    if (Indsæt conditions for hvornår spillet er slut (evt. som en metode)) 
    {
        EndGame();
    }
    
    */
}