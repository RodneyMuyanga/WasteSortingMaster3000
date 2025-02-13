using UnityEngine;
using TMPro;

public class TimerCountDown : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // Reference til timer-UI
    public TextMeshProUGUI levelText;  // Reference til level-UI

    private float timeLeft = 15f;  // Timeren starter på 15 sekunder
    private int level = 1;  // Starter på Level 1

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 15f; // Reset timeren
            level++; // Level op!
            UpdateLevelUI(); // Opdater UI for level
        }

        if (timerText != null)
        {
            timerText.text = "Time left: " + Mathf.Ceil(timeLeft).ToString();
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