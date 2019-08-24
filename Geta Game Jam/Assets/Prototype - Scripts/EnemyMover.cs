using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private float enemySpeed = 1f;
    private Rigidbody2D rb;

    private GameObject closestMinion;
    private GameObject player;

    private bool isFighting = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();

        enemySpeed = Random.Range(0.5f, 1.2f) * enemySpeed;
    }

    private void Update()
    {
        if(!isFighting)
        {

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        } 
    }

    void Attacked(GameObject attacker)
    {
        isFighting = true;
    }

    void StopAttacked(GameObject attacker)
    {
        isFighting = false;
    }
}
