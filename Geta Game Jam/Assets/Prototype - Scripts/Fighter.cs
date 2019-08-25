using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private float m_health;
    private float m_damage;
    private Elements m_element;
    private Elements[] m_strongAgainst;
    private Elements[] m_weakAgainst;
    private float m_attackSpeed;
    private bool isInFight;
    private bool isAtStructure;
    private Fighter m_currentOpponent;
    private BuildingHealthControl m_currentStructure;
    private bool m_isAlive;

    public float Health { get => m_health; set => m_health = value; }
    public float Damage { get => m_damage; set => m_damage = value; }
    public Elements Element { get => m_element; set => m_element = value; }
    public float AttackSpeed { get => m_attackSpeed; set => m_attackSpeed = value; }
    public bool IsInFight { get => isInFight; set => isInFight = value; }
    public bool IsAtStructure { get => isAtStructure; set => isAtStructure = value; }
    public Fighter CurrentOpponent { get => m_currentOpponent; set => m_currentOpponent = value; }
    public BuildingHealthControl CurrentStructure { get => m_currentStructure; set => m_currentStructure = value; }
    public Elements[] StrongAgainst { get => m_strongAgainst; set => m_strongAgainst = value; }
    public Elements[] WeakAgainst { get => m_weakAgainst; set => m_weakAgainst = value; }
    public bool IsAlive { get => m_isAlive; set => m_isAlive = value; }

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

            if(CurrentStructure.gameObject.tag == "Portal")
            {
                InvokeRepeating("TakePortalDamage", 0, AttackSpeed);
            }
        }
    }

    public void StopFight()
    {
        IsInFight = false;
        CurrentOpponent = null;
        CancelInvoke();
    }

    public void AttackStructure()
    {
        CurrentStructure.Attack(Damage);
    }

    public void AttackOpponent()
    {
        if (CurrentOpponent != null)
        {
            if (CurrentOpponent.IsAlive)
            {
                CurrentOpponent.TakeDamage(Damage, Element);
                Debug.Log("Attck");
            }
            else
            {
                StopFight();
            }
        }
    }

    public void TakeDamage(float _damage, Elements _elementType)
    {
        Debug.Log("Got attacked");

        Health -= _damage * ModifiedDamage(_elementType);

        if(Health <= 0)
        {
            Die();
        }
    }

    public void TakePortalDamage()
    {
        Debug.Log("Got attacked");

        Health -= 1;

        if (Health <= 0)
        {
            Die();
        }
    }

    float ModifiedDamage(Elements _elementType)
    {
        float _mod = 1;

        for(int i = 0; i < StrongAgainst.Length; i++)
        {
            if(_elementType == StrongAgainst[i])
            {
                _mod = 0.5f;
            }
        }

        for (int i = 0; i < WeakAgainst.Length; i++)
        {
            if (_elementType == WeakAgainst[i])
            {
                _mod = 2f;
            }
        }

        return _mod;
    }

    void Die()
    {
        IsAlive = false;
        CurrentOpponent.SendMessage("StopAttacked", GetComponent<Minion>());
        gameObject.SetActive(false);        
        StopFight();
    }
}
