using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveManager : MonoBehaviour
{
    
    public WaveSO waveSO;

    public List<ScriptableObject> listOfAvailableWaves;

    public Transform enemySpawnPoint;

    public TextMeshProUGUI countdownTimerText;
    // public float currentTimeValue = 20f;

    [Header("Adjustable Attributes")]
    public float timeBetweenWaves = 4f;
    public float timeBetweenEnemies = 1f;

    [Header("Manual References")]
    public GameObject waveTimer;
    public GameObject waveNumberObj;
    public TextMeshProUGUI waveNumber;
    public GameObject finalWaypoint;

    [Header("Automatic References")]
    public GameObject gameManager;
    public GameManager GM;
    public AudioManager audioManager;
    public Timer waveTimerScript;

    [Header("Variables for script")]
    public int waveID;
    public int wavesFinished;
    public int numOfEnemiesRemaining;

    
    // Starts the first wave and gets all the references to the scripts needed for the WaveManager to function
    private void Start()
    {
        // Starts SpawnEnemies coroutine
        StartCoroutine(SpawnEnemies());

        // Obtaining references for the wave timer and turns off waveTimer
        waveTimer = GameObject.FindGameObjectWithTag("WaveTimer");
        waveTimerScript = waveTimer.GetComponent<Timer>();
        waveTimer.SetActive(false);

        // obtaining references for the UI wave number display and updates the wave display on start
        waveNumberObj = GameObject.FindGameObjectWithTag("WaveNumber");
        waveNumber = waveNumberObj.GetComponent<TextMeshProUGUI>();
        waveNumber.text = (waveID + 1).ToString();

        // Obtaining reference to the GameManager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();
    }

    // Checks which wave is available and spawns the enemies from that particular waveSO
    [ContextMenu("Spawn Wave")]
    public IEnumerator SpawnEnemies()
    {
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            if (waveSO.waveState == WaveState.available)
            {
                waveSO.waveState = WaveState.attacking;
                foreach (GameObject singleEnemy in waveSO.ListOfEnemies)
                {
                    // Checks if the wave is attacking and will: Instantiate an enemy, set its position, play the spawn sound, and wait for the timeBetweenEnemeis
                    if (waveSO.waveState == WaveState.attacking)
                    {
                        Instantiate(singleEnemy);
                        singleEnemy.transform.position = enemySpawnPoint.position;
                        audioManager.PlayEnemySpawnSound();
                        yield return new WaitForSeconds(timeBetweenEnemies);;
                    }
                }
            }
        }
    }

    // Checks if the current wave is finished and increases both the wavemanager's wave ID and wavesFinished counter
    [ContextMenu("Wave status check")]
    public void WaveStatusCheck()
    {
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            foreach (GameObject enemy in waveSO.ListOfEnemies)
            {
                // Checks if the wave is in the state of attacking but every enemy is dead/gone
                if (waveSO.waveState == WaveState.attacking && waveSO.ListOfEnemies.Count == GM.deadEnemyCount)
                {
                    Debug.Log("No enemies in-game, start next wave");

                    // Increases the waveID to trigger next wave and wavesFinished for win condition
                    waveID++;
                    wavesFinished++;

                    // Changes state to finished
                    waveSO.waveState = WaveState.finished;

                    // Toggles the intermission timer (between waves timer)
                    waveTimer.SetActive(true);
                    waveTimerScript.timerStart = true;

                    // Resets the enemy dead count for the next wave
                    GM.deadEnemyCount = 0;
                }

                if (wavesFinished == listOfAvailableWaves.Count)
                {
                    GM.VictoryScreen();
                }
            }
        }
    }

    // Starts the next wave if the wave ID counter in the wavemanager matches the wave ID of the SO
    public void StartNextWave()
    {
            foreach (WaveSO waveSO in listOfAvailableWaves)
            {
                if (waveSO.waveState == WaveState.unavailable && waveSO.waveID == waveID)
                {
                    waveSO.waveState = WaveState.available;
                    StartCoroutine(SpawnEnemies());
                }
            }
    }

    // Resets the game's wave states
    public void ResetWaveState()
    {
        foreach(WaveSO wave in listOfAvailableWaves)
        {
            if (wave.waveID == 0)
            {
                wave.waveState = WaveState.available;
            }
            else
                wave.waveState = WaveState.unavailable;
        }
    }

    // Debug method to increase the death count of the current wave (no purpose in build)
    [ContextMenu("Debug: Increase DeathCount")]
    public void DebugTestIncreaseDeathcount()
    {
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            if (waveSO.waveState == WaveState.attacking)
            {
                GM.deadEnemyCount = waveSO.ListOfEnemies.Count;
                Debug.Log(GM.deadEnemyCount);
            }
        }
    }
}


