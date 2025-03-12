using UnityEngine;
using TMPro;

public class TimerCountDown : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;

    private float timeLeft = 15f;
    private int level = 1;
    private AudioManager audioManager;
    private GroundSpawner groundSpawner; // Reference to GroundSpawner

    public float speedIncreasePerLevel = 0.5f; // Adjust speed increase as needed

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        groundSpawner = FindObjectOfType<GroundSpawner>(); // Find GroundSpawner in scene
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 15f;
            level++;
            UpdateLevelUI();

            if (audioManager != null)
            {
                audioManager.IncreaseMusicPitch();
            }

            if (groundSpawner != null)
            {
                groundSpawner.tileSpeed += speedIncreasePerLevel; // Increase tile speed
            }
        }

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(timeLeft).ToString();
        }
    }

    void UpdateLevelUI()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + level;
        }
    }
}