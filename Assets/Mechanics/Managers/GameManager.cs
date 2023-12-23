// GameManager Class
// This class manages various aspects of the game, including player money, player car, NPCs, UI, and level time.

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    private static GameManager instance;

    // Singleton Property
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("Game Manager is null");
            return instance;
        }
    }

    // Player Money
    [Header("PLAYER MONEY")]
    public int playerMoneyCount;            // Current amount of player money
    public int maxActiveMoneyObjects;       // Maximum allowed active money objects

    // Player Car
    [Header("PLAYER CAR")]
    public GameObject playerCar;            // Reference to the player's car GameObject
    public float playerCarSpeed;            // Speed of the player's car

    // NPC (Non-Player Character)
    [Header("NPC")]
    public int maxActiveNpcCar;             // Maximum allowed active NPC cars
    public float npcSpeed;                  // Speed of NPC cars

    // UI Elements
    [Header("UI")]
    public TextMeshProUGUI moneyCountText;  // Text element displaying player money count
    public TextMeshProUGUI finalScoreText;  // Text element displaying final score

    // Level Time
    [Header("Level Time")]
    public float levelTime;                 // Time limit for the level

    // Awake Method
    private void Awake()
    {
        instance = this;                    // Set the singleton instance to this instance
    }

    // Start Method
    private void Start()
    {
        // Assign player car speed on the first frame
        if (playerCar.GetComponent<IMoveable>() != null)
            playerCar.GetComponent<IMoveable>().carSpeed = playerCarSpeed;

        // Set time scale to normal
        Time.timeScale = 1.0f;
    }

    // StopGame Method
    // Pauses the game by setting the time scale to 0.
    public void StopGame()
    {
        Time.timeScale = 0.0f;
    }
}
