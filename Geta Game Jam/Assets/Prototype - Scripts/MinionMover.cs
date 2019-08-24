using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMover : MonoBehaviour
{
    private float MinionSpeed = 1f;
    private Rigidbody2D rb;

    //private GameObject closestEnemy;
    private GameObject targetPortal;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        MinionSpeed = Random.Range(0.9f, 2f) * MinionSpeed;
        //FindClosestEnemy();

        Debug.DrawRay(transform.position + transform.up, transform.up, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.up, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject.tag == "Portal")
        {
            Debug.Log(hit.collider.gameObject.name);
            targetPortal = hit.collider.gameObject;
        }
        else
        {
            //Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPortal.transform.position, MinionSpeed * Time.deltaTime);
    }

    //private void FindClosestEnemy()
    //{
    //    float minDistance = Mathf.Infinity;
    //    Vector3 currentPos = transform.position;
    //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    //    {
    //        Debug.Log("Enemy Found");
    //        float distance = Vector3.Distance(enemy.transform.position, currentPos);
    //        if (distance < minDistance)
    //        {
    //            closestEnemy = enemy;
    //            minDistance = distance;
    //        }
    //    }
    //}
}
