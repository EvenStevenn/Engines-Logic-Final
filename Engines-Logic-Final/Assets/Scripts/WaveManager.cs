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

    public float timeBetweenWaves = 4f;
    public float timeBetweenEnemies = 1f;

    public int waveID;
    public int wavesFinished;

    public GameObject waveTimer;
    public Timer waveTimerScript;

    public GameObject waveNumberObj;
    public TextMeshProUGUI waveNumber;

    public GameObject finalWaypoint;
    public GameObject gameManager;
    public GameManager GM;

    public AudioManager audioManager;



    
    // Starts the first wave and gets all the references to the scripts needed for the WaveManager to function
    private void Start()
    {
        StartCoroutine(SpawnEnemies());

        // Obtaining references for the wave timer
        waveTimer = GameObject.FindGameObjectWithTag("WaveTimer");
        waveTimerScript = waveTimer.GetComponent<Timer>();
        waveTimer.SetActive(false);

        // obtaining references for the UI wave number display
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
                if (waveSO.waveState == WaveState.attacking && waveSO.ListOfEnemies.Count == GM.deadEnemyCount)
                {
                    Debug.Log("No enemies in-game, start next wave");
                    waveID++;
                    wavesFinished++;
                    waveSO.waveState = WaveState.finished;
                    waveTimer.SetActive(true);
                    waveTimerScript.timerStart = true;
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

    // Debug method to increase the death count of the current wave
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


