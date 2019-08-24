using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float interval;

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

            chosenSpawner = spawners[Random.Range(0, spawners.Length - 1)].transform;

            GameObject enemyClone = Instantiate(enemyPrefab, chosenSpawner.position, chosenSpawner.rotation);
        }
    }
}
