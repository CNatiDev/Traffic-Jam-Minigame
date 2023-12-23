using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The SceneMenuManager class manages scene loading and game quitting.
/// </summary>
public class SceneMenuManager : MonoBehaviour
{
    /// <summary>
    /// Load a scene by name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
