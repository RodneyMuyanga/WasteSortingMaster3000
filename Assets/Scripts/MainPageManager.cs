using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPageManager : MonoBehaviour
{

void Start()
{
    //PlayerPrefs.DeleteAll();
    //PlayerPrefs.Save();   
}

    public TMP_InputField nameInputField;

	public void StartGame()
{
    Debug.Log("StartGame() kaldt!");

    if (nameInputField == null)
    {
        Debug.LogError("Fejl: nameInputField er tom");
        return;
    }

    string playerName = nameInputField.text.Trim(); // Trim fjerner mellemrum i starten/slut
    Debug.Log("Indtastet navn: " + playerName);

    if (!string.IsNullOrEmpty(playerName))
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        Debug.Log("PlayerName gemt: " + playerName);
    }
    else
    {
        Debug.LogError("Fejl: PlayerName er tomt!");
    }

    SceneManager.LoadScene("SceneDesign");
}

    public void ShowHighscores()
    {
        SceneManager.LoadScene("HighscoreScene");
    }
}
