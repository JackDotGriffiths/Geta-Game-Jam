using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVictoryScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryScreen;

    public void EnableVictory()
    {
        victoryScreen.SetActive(true);
    }
}
