using TMPro;
using UnityEngine.Events;
using UnityEngine;
/// <summary>
/// Manages and displays the countdown timer for a game scene.
/// </summary>
public class SceneTimer : MonoBehaviour
{
    /// <summary>
    /// The current time remaining on the timer.
    /// </summary>
    private float currentTime;

    /// <summary>
    /// The TextMeshProUGUI component used to display the time remaining.
    /// </summary>
    public TextMeshProUGUI timeText;

    /// <summary>
    /// Unity Event invoked after the timer reaches zero.
    /// </summary>
    public UnityEvent invokeAfterTime;

    /// <summary>
    /// Initializes and starts the timer.
    /// </summary>
    void Start()
    {
        StartTimer();
    }

    /// <summary>
    /// Updates the timer each frame.
    /// </summary>
    void Update()
    {
        UpdateTimer();
        UpdateTimeText();
    }

    /// <summary>
    /// Initializes the timer with the initial time from the GameManager.
    /// </summary>
    void StartTimer()
    {
        currentTime = GameManager.Instance.levelTime;
    }

    /// <summary>
    /// Updates the timer by decrementing the time each frame.
    /// Invokes events and resets the timer when it reaches zero.
    /// </summary>
    void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            // Display the final score and invoke events
            var gameManager = GameManager.Instance;
            gameManager.finalScoreText.text = StringUtility.FormatMoney(gameManager.playerMoneyCount);
            invokeAfterTime.Invoke();
            currentTime = 0;
        }
    }

    /// <summary>
    /// Updates the TextMeshProUGUI component with the current seconds value.
    /// </summary>
    void UpdateTimeText()
    {
        timeText.text = Mathf.FloorToInt(currentTime).ToString();
    }
}
