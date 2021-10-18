using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that randomly instances and destroys sets of two horizontally-scrolling GameObjects

public class RandomSpawner : MonoBehaviour
{
        
    public GameObject ItemPrefab; // GameObject to spawn as an obstacle
    public float movementSpeed = 30f; // Speed in which the spawned obstacles will move to the left
    public float generationFrequency = 2f; // Time between each spawn in seconds
    public float gapBetweenObstacles = 100; // Vertical distance between the two spawned obstacles
    private const float SPAWN_X_POSITION = 100; // Vertical position to spawn obstacles in
    private const float DESTROY_X_POSITION = -SPAWN_X_POSITION; // Vertical position to spawn obstacles in
    private List<GameObject> ObstacleList; // Internal list to hold currently instanced obstacles
    public float timer = 0; // Internal timer for obstacle spawn

    void Awake()
    {
        // Initialize objectList to keep track of instanced obstacles
        ObstacleList = new List<GameObject>();
    }

    void Update()
    {
        // OBSTACLE SPAWNING
        // Reduces timer each frame
        timer -= Time.deltaTime;
        if (timer < 0) 
        {
            // When timer reaches 0, timer is reset to 'generationFrequency' and obstacle is spawned
            timer += generationFrequency;
            SpawnObstacle(SPAWN_X_POSITION, gapBetweenObstacles);
        }

        // OBSTACLE MOVEMENT
        for(int i = 0; i < ObstacleList.Count; i++) 
        {
            GameObject obstacle = ObstacleList[i];

            // All currently instanced obstacles move left according to 'movementSpeed'
            obstacle.transform.position += new Vector3(-1,0,0) * movementSpeed * Time.deltaTime;

            // Obstacles beyond 'DESTROY_X_POSITION' get destroyed
            if (obstacle.transform.position.x < DESTROY_X_POSITION)
            {
                Destroy(obstacle);
                ObstacleList.Remove(obstacle);
            }
        }
    }

    // Function that spawns obstacles and adds them to 'ObstacleList'
    void SpawnObstacle(float SPAWN_X_POSITION, float gapBetweenObstacles)
    {
        // Two vertically-random vectors are generated with a space between them dictvated by 'gapBetweenObstacles'
        Vector2 bottomVector, topVector;
        bottomVector.x = SPAWN_X_POSITION;
        bottomVector.y = Random.Range(-100, -20);
        topVector.x = SPAWN_X_POSITION;
        topVector.y = bottomVector.y + gapBetweenObstacles;

        // Two GameObjects are instantiated in the vector positions
        GameObject bottomObstacle = Instantiate(ItemPrefab, bottomVector, Quaternion.identity);
        GameObject topObstacle = Instantiate(ItemPrefab, topVector, Quaternion.identity);

        // GameObjects are added to the global 'ObstacleList'
        ObstacleList.Add(bottomObstacle);
        ObstacleList.Add(topObstacle);
    }
}
