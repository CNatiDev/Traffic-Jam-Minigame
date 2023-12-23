using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for spawning cars using an object pool at regular intervals.
/// </summary>
public class BlueTrafficSpawner : MonoBehaviour
{
    /// <summary>
    /// Reference to the object pool for cars.
    /// </summary>
    public ObjectPool carPool;

    /// <summary>
    /// Spawn interval in seconds.
    /// </summary>
    public float spawnTime;
    public Transform dezactivatorPosition;
    public Vector2 spawnInterval = Vector2.zero;
    void Start()
    {
        // Start spawning cars at regular intervals.
        StartCoroutine(SpawnCarsWithInterval());
    }

    /// <summary>
    /// Spawns cars at regular intervals.
    /// </summary>
    IEnumerator SpawnCarsWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            spawnTime = Random.RandomRange(spawnInterval.x, spawnInterval.y);
            if (SpawnerManager.Instance.canSpawn())
                SpawnCar();
        }
    }

    /// <summary>
    /// Spawns a car at the specified spawn point.
    /// </summary>
    void SpawnCar()
    {
        GameObject car = carPool.GetPooledObject();

        if (car.GetComponent<IRotatable>()!=null)
            car.GetComponent<IRotatable>().targetPoint = dezactivatorPosition.position;

        if (car.GetComponent<IMoveable>() != null)
            car.GetComponent<IMoveable>().carSpeed = GameManager.Instance.npcSpeed;

        if (car != null)
        {
            // Set the position and rotation of the spawned car.
            car.transform.position = transform.position;
            car.transform.rotation = transform.rotation;

            // Activate the car.
            car.SetActive(true);
        }
    }

}
