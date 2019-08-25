using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Fighter>())
        {
            collision.gameObject.GetComponent<Fighter>().TakeDamage(1000, Elements.Fire);
        }
    }
}
