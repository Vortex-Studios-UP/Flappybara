using System;
using System.Collections.Generic;
using UnityEngine;

// Script that randomly instances and destroys sets of two horizontally-scrolling GameObjects

public class RandomSpawner : MonoBehaviour
{
    // GameObject to spawn as an obstacle
    public GameObject ItemPrefab;

    // Transform where the obstacles will be stored
    public Transform PoolParent;

    // Transform where the obstacles will be stored
    public int StartingPoolSize = 20;

    // Speed in which the spawned obstacles will move to the left
    [HideInInspector] public float MovementSpeed = 30f;
    // Time between each spawn in seconds
    [HideInInspector] public float GenerationFrequency = 2f;

    // Handles the creation and removal of obstacles efficiently by caching them
    private ObstaclePool pool;

    // Void function that acts upon every object to move it
    private Action<GameObject> obstacleMovement;


    // Horizontal position to spawn obstacles in
    private const float SPAWN_X_POSITION = 100;
    // Horizontal position to spawn obstacles in
    private const float DESTROY_X_POSITION = -2*SPAWN_X_POSITION;
    // Minimum vertical position to spawn obstacles in
    public const float MINIMUM_Y_POSITION = -95;

    // Internal list to hold currently instanced obstacles
    private List<GameObject> ObstacleList;
     // Internal timer for obstacle spawn
    private float timer = 0;
     // Internal counter of spawned obstacles (for difficulty tracking)
    private int obstaclesSpawned = 0;

    // Internal value for vertical distance between the two spawned obstacles
    [HideInInspector] public float gapBetweenObstacles;
    // Value which manages how far apart will an obstacle be vertically with respect to the last, it's itself managed by difficulty
    [HideInInspector] public float distanceFromLastObstacleY;

    // Internal boolean which stores whether or not is this the first obstacle
    private bool isSpawningFirstObstacle = true;

    // Difficulty classes definitions
    public enum Difficulty { VeryEasy, Easy, Medium, Hard, VeryHard };

    void Awake()
    {
        // Initialize objectList to keep track of instanced obstacles.
        ObstacleList = new List<GameObject>();

        // Initialize ObstaclePool 
        pool = new ObstaclePool(ItemPrefab, PoolParent, StartingPoolSize);

        // Cache delegate to avoid GC
        obstacleMovement = ObstacleMovement;
    }

    void Update()
    {
        // OBSTACLE SPAWNING
        // Reduces timer each frame.
        timer -= Time.deltaTime;
        if (timer < 0) 
        {
            // When timer reaches 0, timer is reset to 'generationFrequency' and obstacle is spawned.
            timer += GenerationFrequency;
            SpawnObstacle(SPAWN_X_POSITION, gapBetweenObstacles);
        }

        // OBSTACLE MOVEMENT
        pool.ForEachActive(ref obstacleMovement); // Passed by reference to avoid GC
    }

    // OBSTACLE MOVEMENT
    void ObstacleMovement(GameObject obstacle)
    {
        // All currently instanced obstacles move left according to 'movementSpeed'.
        obstacle.transform.position += new Vector3(-1, 0, 0) * MovementSpeed * Time.deltaTime;

        // Obstacles beyond 'DESTROY_X_POSITION' get destroyed.
        if (obstacle.transform.position.x < DESTROY_X_POSITION)
        {
            pool.Destroy(obstacle);
            ObstacleList.Remove(obstacle);
        }
    }

    // Function that spawns obstacles and adds them to 'ObstacleList'.
    void SpawnObstacle(float SPAWN_X_POSITION, float gapBetweenObstacles)
    {
        // Gets the last Y position spawned in order to spawn the next one close by or far away depending on difficulty
        float lastBottomY = isSpawningFirstObstacle ? -(gapBetweenObstacles/2) : ObstacleList[ObstacleList.Count - 2].transform.position.y;
        
        float nextY = distanceFromLastObstacleY; // The last position will be added to this difference
        bool inFromTop, inFromBottom; // Will this difference fit if moved up/down given the last position?
        inFromTop = distanceFromLastObstacleY + lastBottomY <= -MINIMUM_Y_POSITION - gapBetweenObstacles; // If moved up will it fit?
        inFromBottom = -distanceFromLastObstacleY + lastBottomY >= MINIMUM_Y_POSITION; // If moved down will it fit?
        if (inFromTop && inFromBottom)
            nextY *= Mathf.Sign(UnityEngine.Random.Range(-1.0f, 1.0f)); // If it will fit in both directions, go to any
        else if(inFromTop)
            nextY *= 1; // If it will only fit if moving up, move up
        else if(inFromBottom)
            nextY *= -1; // If it will only fit if moving down, move down
        else
            nextY *= Mathf.Sign(UnityEngine.Random.Range(-1.0f, 1.0f)); // If it won't fit either way, then choose randomly and have it clamped down

        nextY = Mathf.Clamp(nextY + lastBottomY, MINIMUM_Y_POSITION, -MINIMUM_Y_POSITION - gapBetweenObstacles);
        
        // Two vertically-random vectors are generated with a space between them dictvated by 'gapBetweenObstacles'.
        Vector2 bottomVector, topVector;
        bottomVector.x = SPAWN_X_POSITION;
        bottomVector.y = nextY;
        topVector.x = SPAWN_X_POSITION;
        topVector.y = bottomVector.y + gapBetweenObstacles;

        // Two GameObjects are instantiated in the vector positions.
        GameObject bottomObstacle = pool.Instantiate(bottomVector);
        GameObject topObstacle = pool.Instantiate(topVector);

        // GameObjects are added to 'ObstacleList' for tracking.
        ObstacleList.Add(bottomObstacle);
        ObstacleList.Add(topObstacle);

        // 'obstaclesSpawned' is increased to track difficulty.
        obstaclesSpawned++;

        isSpawningFirstObstacle = false;
    }
}
