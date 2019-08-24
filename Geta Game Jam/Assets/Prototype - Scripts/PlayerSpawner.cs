using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elements
{
    Fire, Water, Grass
}

public class PlayerSpawner : MonoBehaviour
{
    public GameObject minionSpawn;
    public GameObject minionPrefab;

    [SerializeField]
    private Elements m_currentElement;

    [SerializeField]
    private Elements[] m_allElements;

    private int m_elementIndex = 0;

    [SerializeField]
    private float m_minionHealth, m_minionAttackSpeed, m_minionDamage;

    // Start is called before the first frame update
    void Start()
    {
        m_currentElement = m_allElements[m_elementIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //Forward
        {
            m_elementIndex++;
            if (m_elementIndex == m_allElements.Length)
            {
                m_elementIndex = 0;
            }
            NewCurrentElement();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //Backwards
        {
            m_elementIndex--;
            if (m_elementIndex == -1)
            {
                m_elementIndex = m_allElements.Length - 1;
            }
            NewCurrentElement();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_elementIndex = 0;
            NewCurrentElement();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_elementIndex = 1;
            NewCurrentElement();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_elementIndex = 2;
            NewCurrentElement();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("spawned");
            GameObject minion = Instantiate(minionPrefab, minionSpawn.transform.position, minionSpawn.transform.rotation);
            minion.GetComponent<Minion>().Health = m_minionHealth;
            minion.GetComponent<Minion>().Damage = m_minionDamage;
            minion.GetComponent<Minion>().AttackSpeed = m_minionAttackSpeed;
        }
    }

    void NewCurrentElement()
    {
        m_currentElement = m_allElements[m_elementIndex];
    }

    //void SetElementToFire()
    //{
    //    m_currentElement = Elements.Fire;
    //}

    //void SetElementToWater()
    //{
    //    m_currentElement = Elements.Water;
    //}

    //void SetElementToGrass()
    //{
    //    m_currentElement = Elements.Grass;
    //}
}
