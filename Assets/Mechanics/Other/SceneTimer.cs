using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SceneTimer : MonoBehaviour
{
    private float currentTime;

    public TextMeshProUGUI timeText;
    public UnityEvent invokeAfterTime;

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
            invokeAfterTime.Invoke();
            currentTime = 0;
        }
    }
    void UpdateTimeText()
    {
        // Update the TextMeshProUGUI component with the current seconds value
        timeText.text = Mathf.FloorToInt(currentTime).ToString();
    }

}
