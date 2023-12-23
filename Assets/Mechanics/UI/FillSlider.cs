using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Controls the filling of a Unity UI Slider over a specified time period.
/// </summary>
public class FillSlider : MonoBehaviour
{
    /// <summary>
    /// The Unity UI Slider component that represents the filling progress.
    /// </summary>
    public Slider gameOverSlider;

    /// <summary>
    /// The time, in seconds, it takes to fill the slider from 0 to 1.
    /// </summary>
    public float fillTime = 5f;

    private Coroutine fillCoroutine;

    /// <summary>
    /// Event invoked after the slider is completely filled.
    /// </summary>
    public UnityEvent executeAfterFill;

    /// <summary>
    /// Initializes the slider to an initial value and sets up the reference to the Slider component.
    /// </summary>
    private void Start()
    {
        // Initialize the slider to an initial value (e.g., zero)
        gameOverSlider.value = 0f;
    }

    /// <summary>
    /// Initiates the process of filling the slider over time.
    /// </summary>
    public void StartFillSlider()
    {
        fillCoroutine = StartCoroutine(FillSliderOverTime());
    }

    /// <summary>
    /// Coroutine that gradually fills the slider over the specified time period.
    /// </summary>
    private IEnumerator FillSliderOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fillTime)
        {
            // Update the slider value based on the elapsed time
            gameOverSlider.value = Mathf.Lerp(0f, 1f, elapsedTime / fillTime);

            // Wait for a frame
            yield return null;

            // Update the elapsed time
            elapsedTime += Time.unscaledDeltaTime;
        }

        // Ensure the slider reaches the maximum value at the end of the time interval
        gameOverSlider.value = 1f;

        // Invoke the event after the fill is complete
        executeAfterFill.Invoke();
    }
}
