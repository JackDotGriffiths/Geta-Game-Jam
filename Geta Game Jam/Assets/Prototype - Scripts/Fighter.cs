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
    private bool isAtStructure;
    private Fighter m_currentOpponent;
    private BuildingHealthControl m_currentStructure;

    public float Health { get => m_health; set => m_health = value; }
    public float Damage { get => m_damage; set => m_damage = value; }
    public Elements Element { get => m_element; set => m_element = value; }
    public float AttackSpeed { get => m_attackSpeed; set => m_attackSpeed = value; }
    public bool IsInFight { get => isInFight; set => isInFight = value; }
    public bool IsAtStructure { get => isAtStructure; set => isAtStructure = value; }
    public Fighter CurrentOpponent { get => m_currentOpponent; set => m_currentOpponent = value; }
    public BuildingHealthControl CurrentStructure { get => m_currentStructure; set => m_currentStructure = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFight(Fighter _oppponent)
    {
        if (!IsInFight)
        {
            IsInFight = true;
            CurrentOpponent = _oppponent;
            InvokeRepeating("AttackOpponent", 0, AttackSpeed);
        }
    }

    public void StartStructureBattle(BuildingHealthControl _structure)
    {
        if (!IsInFight)
        {
            IsInFight = true;
            CurrentStructure = _structure;
            InvokeRepeating("AttackStructure", 0, AttackSpeed);
        }
    }

    public void StopFight()
    {
        IsInFight = false;
        CurrentOpponent = null;
    }

    public void AttackStructure()
    {
        CurrentStructure.Attack(Damage);
    }

    public void AttackOpponent()
    {
        if (CurrentOpponent != null)
        {
            CurrentOpponent.TakeDamage(Damage);
            Debug.Log("Attck");
        }
    }

    public void TakeDamage(float _damage)
    {
        Debug.Log("Got attacked");
        Health -= _damage;

        if(Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
