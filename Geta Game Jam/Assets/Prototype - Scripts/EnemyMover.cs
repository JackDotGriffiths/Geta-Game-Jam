using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour, IPooledObject
{
    private GameObject spawner;

    private float enemySpeed = 0.5f;
    private Rigidbody2D rb;

    private GameObject closestMinion;
    private GameObject player;

    private bool isFighting = false;

    public GameObject Spawner { get => spawner; set => spawner = value; }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();

        enemySpeed = Random.Range(0.5f, 1.2f) * enemySpeed;
    }

    private void Update()
    {
        if(spawner == null)
        {
            Destroy(gameObject);
        }

        if(!isFighting)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFighting = true;
            GetComponent<Enemy>().StartStructureBattle(collision.GetComponent<BuildingHealthControl>());
        }
    }

    void Attacked(Fighter attacker)
    {
        isFighting = true;
        GetComponent<Enemy>().StartFight(attacker);
    }

    void StopAttacked(Fighter attacker)
    {
        isFighting = false;
        GetComponent<Enemy>().StopFight();
    }
}
