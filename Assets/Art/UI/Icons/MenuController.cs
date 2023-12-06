using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject uiMenu;
    [SerializeField] GameObject startMenu;

    private void Start()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        uiMenu.SetActive(false);
        startMenu.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }
    public void OnStart()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        uiMenu.SetActive(true);
        startMenu.SetActive(false);
    }
    public void OnPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        uiMenu.SetActive(false);
        startMenu.SetActive(false);
    }
    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
