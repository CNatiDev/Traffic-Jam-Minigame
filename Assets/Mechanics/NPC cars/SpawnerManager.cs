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
    /// The maximum number of active NPCs in the scene.
    /// </summary>
    public float maxNpcActive;

    /// <summary>
    /// Array of NPC prefabs that can be spawned.
    /// </summary>
    public GameObject[] carNpcs;

    /// <summary>
    /// Flag indicating whether spawning is allowed.
    /// </summary>
    public bool canSpawn;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(Time.time);
    }

    // Update is called once per frame
    private void Update()
    {
        // Find all NPCs in the scene
        carNpcs = GameObject.FindGameObjectsWithTag("Npc");

        // Check if the number of active NPCs exceeds the limit and spawning is allowed
        if (carNpcs.Length > maxNpcActive && canSpawn)
        {
            canSpawn = false;
            Debug.Log(Time.time);
        }
        else
        {
            canSpawn = true;
        }
    }
}
