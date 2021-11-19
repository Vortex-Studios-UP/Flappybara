using System.Collections.Generic;
using UnityEngine;

// Script that randomly instances and destroys horizontally-scrolling GameObjects

public class SimpleRandomSpawner : MonoBehaviour
{
    // GameObject to spawn as an obstacle
    public GameObject ItemPrefab;
    // Speed in which the spawned obstacles will move to the left
    public float MovementSpeed = 30f;
    // Time between each spawn in seconds
    public float GenerationFrequency = 2f;
    // Horizontal position to spawn obstacles in
    public float SpawnXPosition = 100;
    // Maximum vertical position to spawn obstacles in
    public float SpawnYPositionMax = 95;
    // Minimum vertical position to spawn obstacles in
    public float SpawnYPositionMin = -95;

    // Internal list to hold currently instanced obstacles
    private List<GameObject> ElementList;
     // Internal timer for obstacle spawn
    private float timer = 0;


    void Awake()
    {
        // Initialize objectList to keep track of instanced obstacles.
        ElementList = new List<GameObject>();
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
            SpawnObstacle(SpawnXPosition, SpawnYPositionMin, SpawnYPositionMax);
        }

        // OBSTACLE MOVEMENT
        for(int i = 0; i < ElementList.Count; i++) 
        {
            GameObject element = ElementList[i];

            // All currently instanced obstacles move left according to 'movementSpeed'.
            element.transform.position += new Vector3(-1,0,0) * MovementSpeed * Time.deltaTime;

            // Obstacles beyond 'DestroyXPosition' get destroyed.
            if (element.transform.position.x < -SpawnXPosition)
            {
                Destroy(element);
                ElementList.Remove(element);
            }
        }
    }

    // Function that spawns obstacles and adds them to 'ObstacleList'.
    void SpawnObstacle(float SpawnXPosition, float SpawnYPositionMin, float SpawnYPositionMax)
    {
        // A vertically-random vector is generated within the range of SpawnYPositionMin and SpawnYPositionMax
        Vector2 Position;
        Position.x = SpawnXPosition;
        Position.y = Random.Range(SpawnYPositionMin, SpawnYPositionMax);

        // A GameObject is instantiated in the vector position.
        GameObject Element = Instantiate(ItemPrefab, Position, Quaternion.identity);

        // GameObject is added to 'ElementList' for tracking.
        ElementList.Add(Element);
    }
}