using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FUNCTION: This script spawns GameObjects from a Pool defined in an active ObjectPooler.
// NOTES:
// * The GameObjects' spawn position is inherited from this ObjetSpawner's position.
// * An ObjectPooler must be present in scene for this script to function correctly.
// * The actual activation of GameObjects is handled by the ObjectPooler.
// * FixedUpdate is used instead of Update to ensure that spawining is independent from game performance.

public class ObjectSpawner : MonoBehaviour
{
    // Inspector field for the Pool to draw from ObjectPooler.
    [SerializeField] private string poolTag;
    // Inspector field for the time between object spawning.
    [SerializeField] private float spawnTime = 2.5f;

    // Record of how much time has passed since the last spawn.
    private float timeElapsed;

    // The scene's ObjectPooler is retrieved.
    ObjectPooler objectPooler;
    // Start is called before the first frame update.
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // FixedUpdate is called once per fixed-framerate frame.
    void FixedUpdate()
    {
        // Keeps track of time.
        timeElapsed += Time.deltaTime;

        // Spawns an obstacle when (1) the spawn time is reached and (2) the game is still active.
        if (timeElapsed > spawnTime && !GameManager.Instance.isGameOver)
            objectPooler.SpawnFromPool(poolTag, transform.position, Quaternion.identity);
    }
}