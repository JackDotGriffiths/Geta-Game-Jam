using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager m_instance;

    [SerializeField]
    private int m_numberOfAngles;
    [SerializeField]
    private float m_radius;
    private float m_angleToUse;
    [SerializeField]
    private float[] m_angles;

    [SerializeField]
    private Elements[] m_allElements;

    public static GameManager Instance { get => m_instance; set => m_instance = value; }
    public int NumberOfAngles { get => m_numberOfAngles; set => m_numberOfAngles = value; }
    public float AngleToUse { get => m_angleToUse; set => m_angleToUse = value; }
    public float[] Angles { get => m_angles; set => m_angles = value; }
    public float Radius { get => m_radius; set => m_radius = value; }
    public Elements[] AllElements { get => m_allElements; set => m_allElements = value; }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(m_instance.gameObject);
            m_instance = this;
        }

        m_numberOfAngles = PlayerPrefs.GetInt("Channels");

        m_angleToUse = 360f / (float)m_numberOfAngles;

        m_angles = new float[m_numberOfAngles];

        for (int i = 0; i < m_numberOfAngles; i++)
        {
            m_angles[i] = m_angleToUse * i;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
