using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Elements
{
    Fire, Water, Grass
}

public class PlayerSpawner : MonoBehaviour
{
    public GameObject minionSpawn;
    public GameObject minionPrefab;
    private int m_elementIndex = 0;



    [Header("Element Control")]
    [SerializeField]
    private Elements m_currentElement;
    [SerializeField]
    private Elements[] m_allElements;

    [SerializeField]
    private Material waterMat, fireMat, grassMat;


    [Header("Minion Spawning Control")]
    [SerializeField]
    private int m_maximumMinionCount = 20;
    [SerializeField]
    private float m_cooldownTimer = 1;
    [SerializeField]
    private Image cooldownImage;
    [SerializeField]
    private Text minionCounterText;


    [Header("Minion Management")]
    [SerializeField]
    private float m_minionHealth;
    [SerializeField]
    private float m_minionAttackSpeed;
    [SerializeField]
    private float m_minionDamage;

    // Start is called before the first frame update
    void Start()
    {
        m_currentElement = m_allElements[m_elementIndex];
        minionCounterText.text = "0/" + m_maximumMinionCount;

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
            if(GameObject.FindGameObjectsWithTag("Minion").Length < m_maximumMinionCount && cooldownImage.fillAmount == 1)
            {
                SpawnMinion();
            }
        }

        cooldownImage.fillAmount += Time.deltaTime * m_cooldownTimer;
    }

    void NewCurrentElement()
    {
        m_currentElement = m_allElements[m_elementIndex];
    }

    void SpawnMinion()
    {
        cooldownImage.fillAmount = 0;
        minionCounterText.text = (GameObject.FindGameObjectsWithTag("Minion").Length + 1) + "/" + m_maximumMinionCount;


        GameObject minion = Instantiate(minionPrefab, minionSpawn.transform.position, minionSpawn.transform.rotation);
        minion.GetComponent<Minion>().Element = m_currentElement;

        SetStrengthsAndWeaknesses(minion.GetComponent<Minion>());
        SetMinionColour(minion);

        minion.GetComponent<Minion>().Health = m_minionHealth;
        minion.GetComponent<Minion>().Damage = m_minionDamage;
        minion.GetComponent<Minion>().AttackSpeed = m_minionAttackSpeed;
    }

    private void SetStrengthsAndWeaknesses(Fighter minion)
    {
        switch (minion.GetComponent<Minion>().Element)
        {
            case Elements.Fire:
                minion.StrongAgainst = new Elements[] {Elements.Grass};
                minion.WeakAgainst = new Elements[] { Elements.Water };
                break;
            case Elements.Water:
                minion.GetComponent<SpriteRenderer>().material = waterMat;
                minion.StrongAgainst = new Elements[] { Elements.Fire };
                minion.WeakAgainst = new Elements[] { Elements.Grass };
                break;
            case Elements.Grass:
                minion.GetComponent<SpriteRenderer>().material = grassMat;
                minion.StrongAgainst = new Elements[] { Elements.Water };
                minion.WeakAgainst = new Elements[] { Elements.Fire };
                break;
        }
    }

    private void SetMinionColour(GameObject minion)
    {
        switch(minion.GetComponent<Minion>().Element)
        {
            case Elements.Fire:
                minion.GetComponent<SpriteRenderer>().material = fireMat;
                break;
            case Elements.Water:
                minion.GetComponent<SpriteRenderer>().material = waterMat;
                break;
            case Elements.Grass:
                minion.GetComponent<SpriteRenderer>().material = grassMat;
                break;
        }
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
