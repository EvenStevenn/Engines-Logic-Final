﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    public ScriptableObject enemy;
    public TextMeshProUGUI coincount;
    public TextMeshProUGUI countdown;

    //enemy's left in wave? Number of waves remain??
    public TextMeshProUGUI wavestatus;

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

        //foreach (ScriptableObject.enemy in mainScene)
        {
            
        }
    }

    //Start Game/Reload Game function
    public void StartGame()
    {
        SceneManager.LoadScene(mainScene);
        Debug.Log("Loading game level...");
        Time.timeScale = 1f;
    }

    //Return to Main Menu function
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainmenuScene);
        Debug.Log("Loading main menu...");
    }

    //Quit Game function
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

    //Pause menu/camera disable function
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Game is paused.");
        mainCam.SetActive(false);
    }

    //Resume game/camera re-enable function
    public void UnPause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Game is resumed.");
        mainCam.SetActive(true);
    }
}