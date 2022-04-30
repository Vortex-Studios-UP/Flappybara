using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// FUNCTION: This script creates and interacts with the Pool class, which allows GameObject pools to be created and used on scenes.
// NOTES:
// * This script can be used to display foreground and background elements without using to many resources.
// * [System.Serializable] allows a class to be accesible through the Inspector.
// * A singleton pattern is used. No more than one ObjectPooler instance is necessary in each scene.
// * #region makes sections of code collapsable.

public class ObjectPooler : MonoBehaviour
{
    // Pool class is declared.
    [System.Serializable] 
    public class Pool
    {
        // Name of the Pool.
        public string tag;
        // GameObject to store in Pool.
        public GameObject prefab;
        // Size of the Pool.
        public int size;
    }

    #region Singleton
    // The ObjectPooler is made accessible with a singleton pattern.
    public static ObjectPooler Instance;

    // Awake is called before Start.
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // A List of Pools is declared so that a single ObjectPooler object can draw from several Pools.
    // This can be seen in the Inspector as a drop-down menu.
    public List<Pool> pools;

    // New data type is declared: a Dictionary that stores a Tag string for every Queue of GameObjects.
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update.
    void Start()
    {
        // A new poolDictionary is createdin runtime.
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // For each Pool in the list, the appropriate GameObjects are instantiated.
        foreach (Pool pool in pools)
        {
            // objectPool is declared as a way to store every GameObject in a Pool.
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                // Instantiated GameObjects are set to unactive to avoid them showing on Scene.
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // The freshly instantiated pool is added to the Dictionary.
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // This function spawns (activates), dequeues and enqueues GameObjects from a Pool.
    // Returns the spawned GameObject.
    // * tag (string): The tag associated in poolDictionary with the Pool to draw from.
    // * position (Vector2): GameObject Spawn position.
    // * rotation (Quaternion): GameObject Spawn rotation.
    public GameObject SpawnFromPool (string tag, Vector2 position, Quaternion rotation)
    {
        // Safeguard for when the specified tag doesn't exist in poolDictionary.
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // The current GameObject to spawn is dequeued from the specified Pool and stored in this variable.
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // GameObject is spawned in the specified position and rotation.
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // Looks for an implementation of OnObjectSpawn() elsewhere and executes it.
        // The Scroll class implements movement speed OnObjectSpawn().
        // Getting rid of objectToSpawn.GetComponent<IPooledObject>() may make things more performant.
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
            pooledObj.OnObjectSpawn();

        // GameObject is enqueued for later use.
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}