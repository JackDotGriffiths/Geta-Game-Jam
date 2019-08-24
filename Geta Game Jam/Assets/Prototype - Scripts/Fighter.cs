using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private float m_health;
    private float m_damage;
    private Elements m_element;
    private float m_attackSpeed;
    private bool isInFight;
    private Fighter m_currentOpponent;

    public float Health { get => m_health; set => m_health = value; }
    public float Damage { get => m_damage; set => m_damage = value; }
    public Elements Element { get => m_element; set => m_element = value; }
    public float AttackSpeed { get => m_attackSpeed; set => m_attackSpeed = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFight()
    {
        isInFight = true;
    }

    public void StopFight()
    {
        isInFight = false;
    }

    public void AttackStructure()
    {

    }

    public void AttackOpponent()
    {
        m_currentOpponent.TakeDamage(m_damage);
    }

    public void TakeDamage(float _damage)
    {
        m_health -= _damage;

        if(m_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
