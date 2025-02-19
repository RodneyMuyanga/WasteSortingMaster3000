using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPageManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    private void Start()
    {
        // Hvis spilleren allerede har indtastet et navn før, indlæs det
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            nameInputField.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void StartGame()
    {
        // Gem spillerens navn
        string playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save(); // Sikrer at navnet bliver gemt

        // Skift til spilscenen
        SceneManager.LoadScene("SceneDesign");
    }

    public void ShowHighscores()
    {
        // Skift til highscore-scenen (skal laves senere)
        SceneManager.LoadScene("HighscoreScene");
    }
}