using System.Collections;
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

    [SerializeField]
    private float m_enemyHealth, m_enemyAttackSpeed, m_enemyDamage;

    private float nextSpawn;

    private GameObject[] spawners;

    private Transform chosenSpawner;

    [SerializeField]
    private Material waterMat, fireMat, grassMat;

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

            Elements randomElement = GameManager.Instance.AllElements[Random.Range(0, 3)];

            enemyClone.GetComponent<Enemy>().Element = randomElement;

            SetStrengthsAndWeaknesses(enemyClone.GetComponent<Enemy>());

            SetEnemyColour(enemyClone);

            enemyClone.GetComponent<Enemy>().Health = m_enemyHealth;
            enemyClone.GetComponent<Enemy>().Damage = m_enemyDamage;
            enemyClone.GetComponent<Enemy>().AttackSpeed = m_enemyAttackSpeed;

            Destroy(randomSpawnradius);
        }
    }

    private void SetStrengthsAndWeaknesses(Fighter enemy)
    {
        switch (enemy.GetComponent<Enemy>().Element)
        {
            case Elements.Fire:
                enemy.StrongAgainst = new Elements[] { Elements.Grass };
                enemy.WeakAgainst = new Elements[] { Elements.Water };
                break;
            case Elements.Water:
                enemy.GetComponent<SpriteRenderer>().material = waterMat;
                enemy.StrongAgainst = new Elements[] { Elements.Fire };
                enemy.WeakAgainst = new Elements[] { Elements.Grass };
                break;
            case Elements.Grass:
                enemy.GetComponent<SpriteRenderer>().material = grassMat;
                enemy.StrongAgainst = new Elements[] { Elements.Water };
                enemy.WeakAgainst = new Elements[] { Elements.Fire };
                break;
        }
    }

    private void SetEnemyColour(GameObject enemy)
    {
        switch (enemy.GetComponent<Enemy>().Element)
        {
            case Elements.Fire:
                enemy.GetComponent<SpriteRenderer>().material = fireMat;
                break;
            case Elements.Water:
                enemy.GetComponent<SpriteRenderer>().material = waterMat;
                break;
            case Elements.Grass:
                enemy.GetComponent<SpriteRenderer>().material = grassMat;
                break;
        }
    }
}
