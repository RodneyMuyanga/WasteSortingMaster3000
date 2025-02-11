using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
}