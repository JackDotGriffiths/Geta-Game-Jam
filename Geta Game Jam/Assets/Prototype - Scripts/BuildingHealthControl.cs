using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHealthControl : MonoBehaviour
{
    [SerializeField]
    private Image fullHealthBar;

    [SerializeField]
    private int Health = 100;

    [SerializeField]
    private GameObject m_HealthBar;


    private int InitialHealth;

    private void Start()
    {
        InitialHealth = Health;
    }

    void UpdateHealthBar()
    {
        fullHealthBar.fillAmount = (float)Health / (float)InitialHealth;
    }

    public void Attack(float DamageValue)
    {
        Health -= (int)DamageValue;
        UpdateHealthBar();
        if(Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (this.tag == "Portal")
        {
            SpawnManager.Instance.Spawners.Remove(this.gameObject);
        }
        if (m_HealthBar != null)
        {
            Destroy(m_HealthBar);
        }
        Destroy(this.gameObject);
    }
}
