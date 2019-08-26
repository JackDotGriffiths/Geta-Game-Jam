using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    private Vector3 mousePosition;
    private Quaternion targetRot;

    [SerializeField]
    private float rotSpeed;

    #region Angle parameters
    private int m_numberOfAngles;
    private float m_angleToUse;
    private float[] m_angles;
    #endregion

    void Awake()
    {
        m_numberOfAngles = GameManager.Instance.NumberOfAngles;
    }

    void Start()
    {
        m_angleToUse = 360f / (float)m_numberOfAngles;

        m_angles = new float[m_numberOfAngles];

        for (int i = 0; i < m_numberOfAngles; i++)
        {
            m_angles[i] = m_angleToUse * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        targetRot.eulerAngles = new Vector3(0, 0, GetClosestAngle(rot.eulerAngles.z));

        transform.rotation =  Quaternion.Lerp(transform.rotation, targetRot, rotSpeed);
    }

    float GetClosestAngle(float _anglePassed)
    {
        float _closestAngle = Mathf.Infinity;
        int _index = -1;

        if (_anglePassed > (m_angleToUse * m_numberOfAngles) - m_angleToUse / 2)
        {
            return m_angles[0];
        }

        for (int i = 0; i < m_numberOfAngles; i++)
        {
            if (_closestAngle > Mathf.Abs(_anglePassed - m_angles[i]))
            {
                _closestAngle = Mathf.Abs(_anglePassed - m_angles[i]);
                _index = i;
            }
        }

        return m_angles[_index];
    }
}
