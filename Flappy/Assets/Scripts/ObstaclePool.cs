using System;
using System.Collections.Generic;
using UnityEngine;

// Stores a pool of obstacles and provides alternative functionality to Destroy and Instantiate
public class ObstaclePool
{
    // Get size of pool
    public int Count
    {
        get => obstacles.Count;
    }

    private List<GameObject> obstacles; // The pool of obstacles
    private GameObject obstaclePrefab; // The prefab to spawn the pool from
    private Transform parent; // The parent to attach the pool to

    // Constructor that sets spawn place and amount
    public ObstaclePool(GameObject obstaclePrefab, Transform parent, int size)
    {
        obstacles = new List<GameObject>(size);
        this.obstaclePrefab = obstaclePrefab;
        this.parent = parent;
        for (int i = 0; i < size; i++)
        {
            obstacles.Add(GameObject.Instantiate(obstaclePrefab, parent));
            obstacles[i].SetActive(false); // Disable to mark as available
        }
            
    }

    // For anything that has to be performed on every active object
    public void ForEachActive(ref Action<GameObject> f)
    {
        foreach(GameObject x in obstacles)
        {
            if(!x.activeSelf)
                continue;
            f(x);
        }
    }

    // Alternative to GameObject.Destroy, only disabling the object, not calling GC
    public void Destroy(GameObject obstacle)
    {
        obstacle.SetActive(false); // Disable to mark as available
    }

    // Alternative to GameObject.Instantiate, only reenabling objects already pooled which are availabe (not enabled), not calling GC
    public GameObject Instantiate(Vector3 position)
    {
        GameObject ret = Dequeue();
        ret.transform.position = position;
        return ret;
    }

    // Get and enable some object from the pool, effectively delisting it from being available
    private GameObject Dequeue()
    {
        for(int i = 0; i < obstacles.Count; i++)
        {
            if(!obstacles[i].activeSelf)
            {
                obstacles[i].SetActive(true);
                return obstacles[i];
            }
        }
        obstacles.Add(GameObject.Instantiate(obstaclePrefab, parent));
        obstacles[obstacles.Count - 1].SetActive(true);
        return obstacles[obstacles.Count - 1];
    }
}