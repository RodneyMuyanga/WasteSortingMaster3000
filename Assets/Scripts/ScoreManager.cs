using UnityEngine;
using UnityEngine.UI;

//This is just an example, i didn't attack the scrip to unity
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}