﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    [Tooltip("Interval in which enemies will spawn.")]
    private float interval;

    [SerializeField]
    [Tooltip("Spawner Radius determines the radius around a portal in which enemies will randomly spawn.")]
    private float spawnerRadius = 1f;

    [SerializeField]
    [Tooltip("Offset Radius will prevent Enemy getting stuck behind portal. Should always be higher than Spawner Radius")]
    private float offsetRadius = 1.2f;

    private float nextSpawn;

    private GameObject[] spawners;

    private Transform chosenSpawner;

    // Start is called before the first frame update
    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("Portal");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + interval;

            chosenSpawner = spawners[Random.Range(0, spawners.Length)].transform;


            Transform Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            GameObject randomSpawnradius = new GameObject("TempSpawnPosition");
            randomSpawnradius.transform.SetParent(chosenSpawner.transform);
            randomSpawnradius.transform.position = chosenSpawner.position;

            //Offset to prevent stickin'
            randomSpawnradius.transform.position = Vector3.MoveTowards(randomSpawnradius.transform.position, Player.position, offsetRadius);
             
            Vector3 offset = Random.insideUnitCircle * spawnerRadius;
            Vector3 generatedSpawnPosition = chosenSpawner.position + offset;

            GameObject enemyClone = Instantiate(enemyPrefab, generatedSpawnPosition, chosenSpawner.rotation);

            Destroy(randomSpawnradius);




        }
    }
}