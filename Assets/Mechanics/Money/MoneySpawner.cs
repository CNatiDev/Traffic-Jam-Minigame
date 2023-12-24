using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// The MoneySpawner class handles the spawning of money objects within a specified radius.
/// </summary>
public class MoneySpawner : MonoBehaviour
{
    /// <summary>
    /// Prefabs of the money notes to be spawned.
    /// </summary>
    public GameObject[] moneyPrefabs;

    /// <summary>
    /// Time interval for spawning money.
    /// </summary>
    public float spawnInterval = 2f;

    /// <summary>
    /// Radius within which money will be spawned.
    /// </summary>
    public float spawnRadius = 5f;

    /// <summary>
    /// Array to store currently active money objects.
    /// </summary>
    public GameObject[] activeMoney;

    private float timer;  // Countdown timer for spawning money
    private List<GameObject> moneyPool;
    private int randomIndex;

    void Start()
    {
        // Initialize the timer and money pool
        timer = spawnInterval;
        moneyPool = new List<GameObject>();
    }

    void Update()
    {
        // Countdown timer for spawning money
        timer -= Time.deltaTime;

        // Retrieve currently active money objects
        activeMoney = GameObject.FindGameObjectsWithTag("Money");

        // Check if it's time to spawn money and if the maximum number of active money objects is not reached
        if (timer <= 0f && activeMoney.Length < GameManager.Instance.maxActiveMoneyObjects)
        {
            SpawnMoney();
            timer = spawnInterval;  // Reset the timer
        }
    }

    /// <summary>
    /// Spawns a money object within the specified spawn radius.
    /// </summary>
    void SpawnMoney()
    {
        // Get or create money from the object pool
        GameObject money = GetOrCreateMoney();

        // Reset money position within the spawn radius
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
        money.transform.position = new Vector3(randomPosition.x, 1f, randomPosition.y);

        // Activate the money object
        money.SetActive(true);
    }

    /// <summary>
    /// Retrieves or creates a money object from the object pool.
    /// </summary>
    /// <returns>The money object.</returns>
    GameObject GetOrCreateMoney()
    {
        // Check if there is an inactive money object in the pool
        foreach (GameObject money in moneyPool)
        {
            if (!money.activeInHierarchy)
            {
                money.GetComponentInChildren<MoneyValue>().GetComponent<MeshRenderer>().enabled = true;
                money.GetComponentInChildren<MoneyValue>().GetComponent<Collider>().enabled = true;
                return money;
            }
        }

        // If no inactive object found, instantiate a new one and add it to the pool
        randomIndex = Random.Range(0, moneyPrefabs.Length);
        GameObject moneyPrefab = moneyPrefabs[randomIndex];
        GameObject newMoney = Instantiate(moneyPrefab);
        moneyPool.Add(newMoney);


        // Disable the new money object by default
        newMoney.SetActive(false);


        return newMoney;
    }

    /// <summary>
    /// Draws the spawn radius in the Scene view for visualization purposes.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

}
