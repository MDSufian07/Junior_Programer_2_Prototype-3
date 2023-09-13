using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; // Import Unity's Random class

public class spawnManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs; // Create an array to hold your obstacle prefabs
    private Vector3 spawnPosition = new Vector3(30, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;
    private PlayerControl playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControlScript = GameObject.Find("player").GetComponent<PlayerControl>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControlScript.gameOver == false)
        {
            // Get a random index within the array bounds
            int randomIndex = Random.Range(0, ObstaclePrefabs.Length);

            // Instantiate the selected prefab at the spawn position
            GameObject randomObstacle = Instantiate(ObstaclePrefabs[randomIndex], spawnPosition, ObstaclePrefabs[randomIndex].transform.rotation);
            
            // Make sure to set the parent if needed
            randomObstacle.transform.parent = transform;
        }
    }
}
