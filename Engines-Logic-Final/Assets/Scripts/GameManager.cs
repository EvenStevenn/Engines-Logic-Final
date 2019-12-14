using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Manual References")]
    private int nextScene;
    public Button startButton;
    public Button exitButton;
    public Button restartButton;
    public Button mainmenuButton;
    public GameObject pauseMenu;
    public GameObject gamewinMenu;
    public GameObject gameoverMenu;
    public GameObject inGameHUD;
    public GameObject mainCam;
    public string mainScene;
    public string mainmenuScene;
    public bool paused;
    public bool canPause;
    public TextMeshProUGUI livesDisplay;
    public TextMeshProUGUI playerCoinDisplay;

    public int deadEnemyCount;
    public int lives = 3;

    public int playerCurrency = 0;

    //Start Game/Reload Game function
    public void StartGame()
    {
        SceneManager.LoadScene(mainScene);
        Debug.Log("Loading game level...");
        Time.timeScale = 1f;
        canPause = true;
    }

    //Activate/Deactivate "Pause Menu" on Esc.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            UnPause();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && canPause == true)
        {
            Pause();
        }

        livesDisplay.text = lives.ToString();
        playerCoinDisplay.text = playerCurrency.ToString();
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
        Cursor.visible = false;
        Debug.Log("Game is paused.");
        mainCam.GetComponent<CameraScript>().enabled = false;
        canPause = false;
    }

    //Resume game/camera re-enable function
    public void UnPause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Debug.Log("Game is resumed.");
        mainCam.GetComponent<CameraScript>().enabled = true;
        canPause = true;
    }

    //Activate Victory Screem
    public void VictoryScreen()
    {
        Time.timeScale = 0f;
        gamewinMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Level cleared. You win!");
        mainCam.GetComponent<CameraScript>().enabled = false;
        canPause = false;
    }

    //Activate LoseScreen
    public void LossScreen()
    {

        Time.timeScale = 0f;
        gameoverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Level failed. You lose!");
        mainCam.GetComponent<CameraScript>().enabled = false;

    }

    public void UpdatePlayerCurrencyCount()
    {
        playerCoinDisplay.text = playerCurrency.ToString();
    }
}