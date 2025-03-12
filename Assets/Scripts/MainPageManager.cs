using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPageManager : MonoBehaviour
{

void Start()
{
    //PlayerPrefs.DeleteAll();
    //PlayerPrefs.Save();
	Debug.Log("PlayerName fra PlayerPrefs: " + PlayerPrefs.GetString("PlayerName", "Ingen navn fundet"));
}

    public TMP_InputField nameInputField;

    public void StartGame()
{
    string playerName = nameInputField.text.Trim(); // Trim fjerner mellemrum før og efter tekst

    if (!string.IsNullOrEmpty(playerName)) 
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        Debug.Log("Spillernavn gemt: " + playerName);
    }
    else
    {
        Debug.LogError("Fejl: Ingen spillernavn indtastet!");
    }

    SceneManager.LoadScene("SceneDesign");
}


    public void ShowHighscores()
    {
        SceneManager.LoadScene("HighscoreScene");
    }
}
