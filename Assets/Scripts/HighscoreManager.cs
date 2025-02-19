using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    public static List<(string playerName, int score)> highscoreList = new List<(string, int)>();

    void Start()
    {
        LoadHighscores();
        DisplayHighscores();
    }

    void LoadHighscores()
    {
        highscoreList.Clear();
        for (int i = 0; i < 10; i++)
        {
            string nameKey = "Highscore_Name_" + i;
            string scoreKey = "Highscore_Score_" + i;

            if (PlayerPrefs.HasKey(nameKey) && PlayerPrefs.HasKey(scoreKey))
            {
                string name = PlayerPrefs.GetString(nameKey);
                int score = PlayerPrefs.GetInt(scoreKey);
                highscoreList.Add((name, score));
            }
        }
    }

    void DisplayHighscores()
    {
        highscoreText.text = "Top 10 Highscores:\n";
        int rank = 1;
        foreach (var entry in highscoreList)
        {
            highscoreText.text += rank + ". " + entry.playerName + " - " + entry.score + "\n";
            rank++;
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainPage");
    }
}