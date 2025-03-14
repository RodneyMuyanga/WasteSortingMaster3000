using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

public void EndGame()
{
    string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");

    if (playerName == "Unknown")
    {
        Debug.LogError("Fejl: PlayerName blev ikke fundet i PlayerPrefs!");
    }
    else
    {
        Debug.Log("Henter PlayerName fra PlayerPrefs: " + playerName);
    }

    int finalScore = score;

    if (!PlayerPrefs.HasKey("HighscoreKeys"))
    {
        PlayerPrefs.SetString("HighscoreKeys", playerName);
    }
    else
    {
        string existingKeys = PlayerPrefs.GetString("HighscoreKeys");
        if (!existingKeys.Contains(playerName))
        {
            PlayerPrefs.SetString("HighscoreKeys", existingKeys + "|" + playerName);
        }
    }

    PlayerPrefs.Save();

    // Find HighscoreManager og opdater highscore
    HighScoreManager highScoreManager = FindObjectOfType<HighScoreManager>();
    if (highScoreManager != null)
    {
        Debug.Log("HighscoreManager fundet, opdaterer score...");
        highScoreManager.UpdateHighscore(playerName, finalScore);
    }
    else
    {
        Debug.LogError("Fejl: HighscoreManager ikke fundet i scenen!");
    }

    SceneManager.LoadScene("HighscoreScene");
}

}
