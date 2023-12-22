using UnityEngine;

/// <summary>
/// Manages the spawning of non-player controlled characters (NPCs) in the game.
/// </summary>
public class SpawnerManager : MonoBehaviour
{
    // Singleton instance
    private static SpawnerManager instance;

    /// <summary>
    /// Gets the singleton instance of the SpawnerManager.
    /// </summary>
    public static SpawnerManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("SpawnerManager instance is null");
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Array of NPC prefabs that can be spawned.
    /// </summary>
    public GameObject[] carNpcs;

    /// <summary>
    /// Checks if it is currently allowed to spawn a new non-player controlled character (NPC) based on the maximum active NPC limit set by the GameManager.
    /// </summary>
    /// <returns>
    /// Returns true if spawning is allowed (i.e., the current number of active NPCs is below the maximum limit), and false otherwise.
    /// </returns>
    public bool canSpawn()
    {
        // Find all active NPCs in the scene with the "Npc" tag
        carNpcs = GameObject.FindGameObjectsWithTag("Npc");

        // Check if the number of active NPCs exceeds the limit specified by GameManager.Instance.maxActiveNpcCar - 1
        // Note: The "- 1" accounts for zero-based indexing.
        if (carNpcs.Length > GameManager.Instance.maxActiveNpcCar - 1)
        {
            // Return false if spawning is not allowed
            return false;
        }

        // Return true if spawning is allowed
        return true;
    }

}
