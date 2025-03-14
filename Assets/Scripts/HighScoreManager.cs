using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    private static List<(string playerName, int score)> highscoreList = new List<(string, int)>();

    void Start()
    {
        LoadHighScores();
        DisplayHighScores();
    }

    void LoadHighScores()
{
    highscoreList.Clear();

    string existingKeys = PlayerPrefs.GetString("HighscoreKeys", ""); // Henter listen af spillere
    foreach (string playerName in existingKeys.Split('|'))
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            int score = PlayerPrefs.GetInt("Highscore_" + playerName, 0);
            highscoreList.Add((playerName, score));
        }
    }
}


public void UpdateHighscore(string playerName, int newScore)
{
    string highscoreKey = "Highscore_" + playerName; // Unik nøgle til hver spiller
    int currentHighscore = PlayerPrefs.GetInt(highscoreKey, 0); // Hent eksisterende score

    if (newScore > currentHighscore) // Kun opdater, hvis ny score er højere
    {
        PlayerPrefs.SetInt(highscoreKey, newScore);
        PlayerPrefs.Save();
    }
}


    void DisplayHighScores()
{
    if (highscoreText == null)
    {
        Debug.LogError("HighScoreManager: HighscoreText er ikke sat i Inspector!");
        return;
    }

    if (highscoreList.Count == 0)
    {
        highscoreText.text = "Ingen highscores endnu!";
        return;
    }

    int rank = 1;
    foreach (var entry in highscoreList)
    {
        highscoreText.text += rank + ". " + entry.playerName + " - " + entry.score + "\n";
        rank++;
    }
}

}
