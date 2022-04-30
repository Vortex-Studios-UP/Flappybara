using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * FUNCTION: This script instantiates objects and repositions them when they go off-screen.

public class ObjectPool : MonoBehaviour
{
    // Inspector field for obstacle to instantiate.
    [SerializeField] private GameObject obstaclePrefab;

    // Inspector field for maximum amount of instanced objects.
    [SerializeField] private int poolSize = 5;

    // Inspector field for the time between object spawning.
    [SerializeField] private float spawnTime = 2.5f;

    // Inspector field for the horizontal spawn location.
    [SerializeField] private float xSpawnPosition = 12f;

    // Inspector field for the vertical spawn location range.
    [SerializeField] private float minYPosition = -2f;
    [SerializeField] private float maxYPosition = 3f;

    // Record of how much time has passed since the last spawn.
    private float timeElapsed;
    // Record of how many objects have been spawned.
    private int obstacleCount;
    // List of active obstacles.
    private GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        // Declares a list of objects to store.
        obstacles = new GameObject[poolSize];

        // Instantiates the specified unactive amount of objects.
        for(int i = 0; i < poolSize; i++)
        {
            obstacles[i] = Instantiate(obstaclePrefab);
            obstacles[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Keeps track of time.
        timeElapsed += Time.deltaTime;

        // Spawns an obstacle when (1) the spawn time is reached and (2) the game is still active.
        if (timeElapsed > spawnTime && !GameManager.Instance.isGameOver)
            SpawnObstacle();
    }

    // SpawnObstacle activates a new obstacle when enough time has passed.
    private void SpawnObstacle()
    {
        // Initializes time elapsed.
        timeElapsed = 0f;

        // Sets a spawn position on the specified X value and Y range.
        float ySpawnPosition = Random.Range(minYPosition, maxYPosition);
        Vector2 spawnPosition = new Vector2(xSpawnPosition, ySpawnPosition);
        obstacles[obstacleCount].transform.position = spawnPosition;

        // Activates the obstacles that are not active.
        if (!obstacles[obstacleCount].activeSelf)
            obstacles[obstacleCount].SetActive(true);

        // Increases the obstacle count.
        obstacleCount++;

        // When the count reaches the pool size, the cycle is started anew.
        if (obstacleCount == poolSize)
            obstacleCount = 0;  
    }
}
