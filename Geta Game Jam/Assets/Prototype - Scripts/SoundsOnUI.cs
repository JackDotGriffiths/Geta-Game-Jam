using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnUI : MonoBehaviour
{
    public void PlayButtonSound()
    {
        AudioManager.instance.Play("Button");
    }
}
