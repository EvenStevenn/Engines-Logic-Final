using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int nextScene;
    public Button startButton;
    public Button exitButton;
    public Button restartButton;
    public Button mainmenuButton;
    public GameObject pauseMenu;
    public GameObject inGameHUD;
    public GameObject mainCam;
    public string mainScene;
    public string mainmenuScene;
    public bool paused;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            UnPause();
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(mainScene);
        Debug.Log("Loading game level...");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainmenuScene);
        Debug.Log("Loading main menu...");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Game is paused.");
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Game is resumed.");
    }
}