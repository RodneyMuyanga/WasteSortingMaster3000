using UnityEngine;
using TMPro;

public class TimerCountDown : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // UI-element til at vise timeren
    private float timeLeft = 15f;      // Starttid på 15 sekunder

    void Update()
    {
        timeLeft -= Time.deltaTime; // Reducer tid i sekunder
        if (timeLeft <= 0)
        {
            timeLeft = 15f;  // Reset timeren til 15 sekunder
        }
        timerText.text = "Tid: " + Mathf.Ceil(timeLeft).ToString(); // Opdater UI
    }
}