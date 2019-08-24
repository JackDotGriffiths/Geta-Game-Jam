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


    private int InitialHealth;

    private void Start()
    {
        InitialHealth = Health;
    }

    void UpdateHealthBar()
    {
        fullHealthBar.fillAmount = Health / InitialHealth;
    }

    public void Attack(int DamageValue)
    {
        Health -= DamageValue;
        UpdateHealthBar();
    }
}
