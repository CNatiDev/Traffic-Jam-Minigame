using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAfterFill : MonoBehaviour
{
    private Slider gameOverSlider;
    public float fillTime = 5f; // Time interval in seconds to fill the slider to the maximum
    public string sceneName;
    private void Start()
    {
        // Make sure you have a reference to the Slider component attached to the object
        gameOverSlider = GetComponent<Slider>();

        // Initialize the slider to an initial value (e.g., zero)
        gameOverSlider.value = 0f;
    }

    public void StartFillSlider()
    {
        StartCoroutine(FillSliderOverTime());
    }

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
            elapsedTime += Time.deltaTime;
        }

        // Make sure the slider reaches the maximum value at the end of the time interval
        gameOverSlider.value = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
