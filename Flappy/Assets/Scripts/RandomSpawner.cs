using System.Collections.Generic;
using UnityEngine;

// Script that randomly instances and destroys sets of two horizontally-scrolling GameObjects

public class RandomSpawner : MonoBehaviour
{
    // GameObject to spawn as an obstacle
    public GameObject ItemPrefab;
    // Speed in which the spawned obstacles will move to the left
    [HideInInspector] public float MovementSpeed = 30f;
    // Time between each spawn in seconds
    [HideInInspector] public float GenerationFrequency = 2f;
    /*
    // Maximum gap size (when Difficulty is VeryEasy)
    public float MaxGapSize = 130f;
    // Amount by which the gap size decreases with each Difficulty level
    public float GapDifficultyDecrease = 3f;
    // Amount of spawned pipes that increase Difficulty level
    public int DifficultyStep = 10;
    */

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
    // SetDifficulty() modifies this value according to 'maxGapSize', 'minGapSize' and 'obstaclesSpawned'
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

        // Set Difficulty to VeryEasy
        //SetDifficulty(Difficulty.VeryEasy);
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
        for(int i = 0; i < ObstacleList.Count; i++) 
        {
            GameObject obstacle = ObstacleList[i];

            // All currently instanced obstacles move left according to 'movementSpeed'.
            obstacle.transform.position += new Vector3(-1,0,0) * MovementSpeed * Time.deltaTime;

            // Obstacles beyond 'DESTROY_X_POSITION' get destroyed.
            if (obstacle.transform.position.x < DESTROY_X_POSITION)
            {
                Destroy(obstacle);
                ObstacleList.Remove(obstacle);
            }
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
            nextY *= Mathf.Sign(Random.Range(-1.0f, 1.0f)); // If it will fit in both directions, go to any
        else if(inFromTop)
            nextY *= 1; // If it will only fit if moving up, move up
        else if(inFromBottom)
            nextY *= -1; // If it will only fit if moving down, move down
        else
            nextY *= Mathf.Sign(Random.Range(-1.0f, 1.0f)); // If it won't fit either way, then choose randomly and have it clamped down

        nextY = Mathf.Clamp(nextY + lastBottomY, MINIMUM_Y_POSITION, -MINIMUM_Y_POSITION - gapBetweenObstacles);

        // Two vertically-random vectors are generated with a space between them dictvated by 'gapBetweenObstacles'.
        Vector2 bottomVector, topVector;
        bottomVector.x = SPAWN_X_POSITION;
        bottomVector.y = nextY;
        topVector.x = SPAWN_X_POSITION;
        topVector.y = bottomVector.y + gapBetweenObstacles;

        // Two GameObjects are instantiated in the vector positions.
        GameObject bottomObstacle = Instantiate(ItemPrefab, bottomVector, Quaternion.identity);
        GameObject topObstacle = Instantiate(ItemPrefab, topVector, Quaternion.identity);

        // GameObjects are added to 'ObstacleList' for tracking.
        ObstacleList.Add(bottomObstacle);
        ObstacleList.Add(topObstacle);

        // 'obstaclesSpawned' is increased to track difficulty.
        obstaclesSpawned++;

        // New difficulty is set.
        //SetDifficulty(GetDifficulty());

        isSpawningFirstObstacle = false;
    }
    /*
    // Function that increased Difficulty each time 10 new obstacles have been spawned.
    Difficulty GetDifficulty()
    {
        if (obstaclesSpawned >= DifficultyStep*4) return Difficulty.VeryHard;
        if (obstaclesSpawned >= DifficultyStep*3) return Difficulty.Hard;
        if (obstaclesSpawned >= DifficultyStep*2) return Difficulty.Medium;
        if (obstaclesSpawned >= DifficultyStep) return Difficulty.Easy;
        return Difficulty.VeryEasy;
    }

    // Function that sets the GapBetweenObstacles by dividing the GapRange in five intervals.
    void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.VeryEasy:
                gapBetweenObstacles = MaxGapSize;
                break;
            case Difficulty.Easy:
                gapBetweenObstacles = MaxGapSize - GapDifficultyDecrease;
                break;
            case Difficulty.Medium:
                gapBetweenObstacles = MaxGapSize - 2*GapDifficultyDecrease;
                break;
            case Difficulty.Hard:
                gapBetweenObstacles = MaxGapSize - 3*GapDifficultyDecrease;
                break;
            case Difficulty.VeryHard:
                gapBetweenObstacles = MaxGapSize - 4*GapDifficultyDecrease;
                break;
        }
    }
    */
}
