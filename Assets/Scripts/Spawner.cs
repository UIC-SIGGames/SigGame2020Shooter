﻿using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyTypes;

    private Transform player;
    private float nextSpawnTimer = 0f;
    private float nextSpawnID = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(nextSpawnTimer <= 0)
        {
            SpawnRandom(DetermineSpawnPosition());
            nextSpawnTimer = 5f;
        }

        nextSpawnTimer -= Time.deltaTime;
    }

    private void SpawnRandom(Vector3 spawnPos)
    {
        var newEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnPos, Quaternion.identity);
        newEnemy.name += " " + nextSpawnID++;
    }

    private Vector3 DetermineSpawnPosition()
    {
        return player.position + new Vector3(Random.Range(5, 10), 0, Random.Range(5, 10));
    }
}