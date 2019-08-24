using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Fighter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInFight)
        {
            InvokeRepeating("AttackOpponent", 0, AttackSpeed);
        }

        if (IsAtStructure)
        {
            InvokeRepeating("AttackStructure", 0, AttackSpeed);
        }
    }
}
