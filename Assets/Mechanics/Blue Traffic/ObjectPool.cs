using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages an object pool for efficient instantiation and deactivation of objects.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// The prefab to be used for object instantiation.
    /// </summary>
    public GameObject prefabToPool;

    /// <summary>
    /// The initial size of the object pool.
    /// </summary>
    public int poolSize = 10;

    private List<GameObject> objectPool;

    void Start()
    {
        // Initialize the object pool on script start.
        InitializeObjectPool();
    }

    /// <summary>
    /// Initializes the object pool by instantiating and deactivating a predefined number of objects.
    /// </summary>
    void InitializeObjectPool()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabToPool);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    /// <summary>
    /// Retrieves an inactive object from the pool or creates a new one if none are available.
    /// </summary>
    /// <returns>An inactive object from the pool.</returns>
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        // If no inactive object is found, create a new one and add it to the pool.
        GameObject newObj = Instantiate(prefabToPool);
        newObj.SetActive(false);
        objectPool.Add(newObj);

        return newObj;
    }
}
