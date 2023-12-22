using UnityEngine;
using System.Collections.Generic;

public class MoneySpawner : MonoBehaviour
{
    public GameObject[] moneyPrefabs; // Prefab of the money note
    public float spawnInterval = 2f; // Time interval for spawning money
    public float spawnRadius = 5f; // Radius within which money will be spawned
    public GameObject[] activeMoney;

    int randomIndex = 0;
    private float timer;
    private List<GameObject> moneyPool;

    void Start()
    {
        timer = spawnInterval;
        moneyPool = new List<GameObject>();
    }

    void Update()
    {
        // Countdown timer for spawning money
        timer -= Time.deltaTime;

        activeMoney = GameObject.FindGameObjectsWithTag("Money");
        if (timer <= 0f)
        {   if (activeMoney.Length > GameManager.Instance.maxActiveMoneyObjects - 1) return;
            SpawnMoney();
            timer = spawnInterval;
        }

    }

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

    GameObject GetOrCreateMoney()
    {
        // Check if there is an inactive money object in the pool
        foreach (GameObject money in moneyPool)
        {
            if (!money.activeInHierarchy)
                return money;
        }

        randomIndex = Random.RandomRange(0, moneyPrefabs.Length);
        GameObject moneyPrefab = moneyPrefabs[randomIndex];
        // If no inactive object found, instantiate a new one and add it to the pool
        GameObject newMoney = Instantiate(moneyPrefab);
        moneyPool.Add(newMoney);

        // Disable the new money object by default
        newMoney.SetActive(false);

        return newMoney;
    }
    void OnDrawGizmos()
    {
        // Draw spawn radius in Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
