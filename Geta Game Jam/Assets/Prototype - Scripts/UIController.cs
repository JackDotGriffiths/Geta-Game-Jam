using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    [Header("Panels")]
    private GameObject titleScreenPanel;
    [SerializeField]
    private GameObject levelSelectPanel;

    [SerializeField]
    private Animator levelSelectAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevelSelect()
    {
        levelSelectAnimator.Play("LevelSelectScreenIn");
    }

    public void GoToTitleScreen()
    {
        levelSelectAnimator.Play("LevelSelectScreenOut");
    }

    public void Loadlevel(int numberOfChannels)
    {
        PlayerPrefs.SetInt("Channels", numberOfChannels);
        SceneManager.LoadScene("MainGame");
    }

    void Quit()
    {
        Application.Quit();
    }


}

