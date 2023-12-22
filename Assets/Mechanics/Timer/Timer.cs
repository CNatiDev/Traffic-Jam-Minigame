using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime;

    public TextMeshProUGUI timeText;
    public GameObject gameOverPanel;
    public LoadSceneAfterFill sliderTimer;
    void Start()
    {
        // Start the timer
        StartTimer();
    }

    void Update()
    {
        // Update the timer each frame
        UpdateTimer();

        // Display the seconds and next 2 milliseconds in the TextMeshProUGUI component
        UpdateTimeText();

        // For debugging, you can still log the values to the console
        Debug.Log($"Seconds: {Mathf.FloorToInt(currentTime)} | Milliseconds: {(currentTime % 1) * 1000:F2}");
    }

    void StartTimer()
    {
        currentTime = GameManager.Instance.levelTime;
    }

    void UpdateTimer()
    {
        // If the timer is greater than 0, decrement it each frame
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            var gameManager = GameManager.Instance;
            gameManager.finalScoreText.text = StringUtility.FormatMoney(gameManager.playerMoneyCount);
            gameOverPanel.SetActive(true);
            sliderTimer.StartFillSlider();
            currentTime = 0;
        }
    }
    void UpdateTimeText()
    {
        // Update the TextMeshProUGUI component with the current seconds value
        timeText.text = Mathf.FloorToInt(currentTime).ToString();
    }

}
