using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private static PauseMenu m_instance;

    private bool isPaused;

    public static PauseMenu Instance { get => m_instance; set => m_instance = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    private void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            TogglePauseState();
        }
    }

    private void TogglePauseState()
    {
        if(isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if(!isPaused)
        {
            pauseMenu.SetActive(false);          
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        isPaused = !isPaused;

        TogglePauseState();
    }

    public void ExitToMain()
    {
        isPaused = false;
        SceneManager.LoadScene("TitleScreen");
    }
}
