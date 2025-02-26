using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This is just an example, i didn't attack the scrip to unity
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI boosterText;
    [SerializeField] private int score = 0;
    [SerializeField] private AudioSource scoreSound;
    
    private int streakCount = 0; 
    private const int streakThreshold = 5; 
    private const int streakBonus = 20; 

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
        // Skjul booster-besked fra start
        if (boosterText != null)
        {
            boosterText.text = "";
        }
    }
    public void AddScore(int value)
    {
        score += value;
        streakCount++;

        if (streakCount >= streakThreshold)
        {
            score += streakBonus;
            ShowBoosterMessage(); 
            streakCount = 0;
        }
        
        Debug.Log("Score: " + score);
       UpdateScoreUI();
       
       if (scoreSound != null)
       {
           scoreSound.Play();
       }
       else
       {
              Debug.LogWarning("Score sound is not set!");
       }
    }
    
    public void ResetStreak()
    {
        streakCount = 0; // Nulstil streak hvis spilleren misser
        Debug.Log("❌ Streak nulstillet!");
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    
    private void ShowBoosterMessage()
    {
        if (boosterText != null)
        {
            boosterText.text = "🔥 BOOSTER AKTIVERET! +20 POINT 🔥";
            Invoke("HideBoosterMessage", 2f); // Skjul efter 2 sekunder
        }
    }

    private void HideBoosterMessage()
    {
        if (boosterText != null)
        {
            boosterText.text = "";
        }
    }
}