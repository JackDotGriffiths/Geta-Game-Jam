using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private GameObject m_particleEffect;
    private GameObject m_currParticleEffect;

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
    public GameObject ParticleEffect { get => m_particleEffect; set => m_particleEffect = value; }
    public GameObject deathParticleEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Updating");
        if (CurrentOpponent != null && CurrentOpponent.isActiveAndEnabled)
        {
            Debug.Log("Looking");
            gameObject.transform.LookAt(CurrentOpponent.gameObject.transform);
        }
    }

    private void OnEnable()
    {
        IsInFight = false;
        CurrentOpponent = null;
        CurrentStructure = null;
        IsAtStructure = false;
    }

    public void ParticlesOn()
    {
        m_currParticleEffect = Instantiate(m_particleEffect, this.gameObject.transform);
        Invoke("ParticlesOff", 0.5f);
    }

    public void ParticlesOff()
    {
        Destroy(m_currParticleEffect);
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
        if(gameObject.GetComponent<EnemyMover>() != null)
        {
            gameObject.GetComponent<EnemyMover>().StopAttacked();
        }
        else
        {
            gameObject.GetComponent<MinionMover>().StopAttacked();
        }
        IsInFight = false;
        CurrentOpponent = null;
        CancelInvoke();
    }

    public void AttackStructure()
    {
        CurrentStructure.Attack(Damage);
        int _index = Random.Range(0, 4);
        switch (_index)
        {
            case 0:
                AudioManager.instance.Play("Pew1");
                break;
            case 1:
                AudioManager.instance.Play("Pew2");
                break;
            case 2:
                AudioManager.instance.Play("Pew3");
                break;
            case 3:
                AudioManager.instance.Play("Pew4");
                break;

        }
        ParticlesOn();
    }

    public void AttackOpponent()
    {
        ParticlesOn();
        if (CurrentOpponent.isActiveAndEnabled)
        {
            if (CurrentOpponent.IsAlive)
            {
                CurrentOpponent.TakeDamage(Damage, Element);
                int _index = Random.Range(0, 4);
                switch (_index)
                {
                    case 0:
                        AudioManager.instance.Play("Pew1");
                        break;
                    case 1:
                        AudioManager.instance.Play("Pew2");
                        break;
                    case 2:
                        AudioManager.instance.Play("Pew3");
                        break;
                    case 3:
                        AudioManager.instance.Play("Pew4");
                        break;

                }
                Debug.Log("Attck");
            }
            else
            {
                StopFight();
            }
        }
        else
        {
            StopFight();
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
        int _index = Random.Range(0, 3);
        switch (_index)
        {
            case 0:
                AudioManager.instance.Play("Pop1");
                break;
            case 1:
                AudioManager.instance.Play("Pop2");
                break;
            case 2:
                AudioManager.instance.Play("Pop3");
                break;

        }
        Instantiate(deathParticleEffect, transform.position, transform.rotation);
        IsAlive = false;
        if (CurrentOpponent != null && CurrentOpponent.isActiveAndEnabled)
        {
            CurrentOpponent.SendMessage("StopAttacked");
        }
        StopFight();
        gameObject.SetActive(false);        
    }
}
