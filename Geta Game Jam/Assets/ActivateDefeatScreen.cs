using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDefeatScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject defeatScreen;

    public void EnableDefeat()
    {
        defeatScreen.SetActive(true);
    }
}
