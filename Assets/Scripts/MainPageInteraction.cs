using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageInteraction: MonoBehaviour
{
    void OnMouseDown()
    {
        if (gameObject.name == "StartGameSign")
        {
            SceneManager.LoadScene("SceneDesign");
        }
        else if (gameObject.name == "HighscoreSign")
        {
            SceneManager.LoadScene("HighscoreScene");
        }
    }
}