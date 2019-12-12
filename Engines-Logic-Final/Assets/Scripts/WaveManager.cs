using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveManager : MonoBehaviour
{
    public EnemySO enemySO;
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

    public AudioManager audioManager;

    public GameObject gamewinMenu;
    public GameObject gameoverMenu;

    public Camera mainCam;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        waveTimer = GameObject.FindGameObjectWithTag("WaveTimer");
        waveTimerScript = waveTimer.GetComponent<Timer>();
        waveTimer.SetActive(false);
        waveNumberObj = GameObject.FindGameObjectWithTag("WaveNumber");
        waveNumber = waveNumberObj.GetComponent<TextMeshProUGUI>();
        waveNumber.text = (waveID + 1).ToString();
    }



    void Update()
    {
        WaveStatusCheck();


        //Lose game condition
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            foreach (EnemySO singleEnemy in waveSO.ListOfEnemies)
            {
                
                if (singleEnemy.enemyPrefab.transform.position == finalWaypoint.transform.position)
                {
                    mainCam = Camera.main;
                    Time.timeScale = 0f;
                    gameoverMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Debug.Log("Level failed. You lose!");
                    mainCam.enabled = false;
                }
            }
        }
    }

    [ContextMenu("Spawn Wave")]
    public IEnumerator SpawnEnemies()
    {
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            if (waveSO.waveState == WaveState.available && waveSO.deadEnemyCount == 0)
            {
                waveSO.waveState = WaveState.attacking;
                foreach (EnemySO singleEnemy in waveSO.ListOfEnemies)
                {
                    if (waveSO.waveState == WaveState.attacking)
                    {
                        Instantiate(singleEnemy.enemyPrefab);
                        singleEnemy.enemyPrefab.transform.position = enemySpawnPoint.position;
                        audioManager.PlayEnemySpawnSound();
                        yield return new WaitForSeconds(timeBetweenEnemies);
                        Debug.Log(waveSO.ListOfEnemies.Count);
                    }
                }
            }
        }
    }

    /*public IEnumerator WaitTimeBetweenWaves()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

    }*/

    [ContextMenu("Wave status check")]
    public void WaveStatusCheck()
    {
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            foreach (EnemySO enemySO in waveSO.ListOfEnemies)
            {
                if (enemySO.health <= 0)
                {
                    enemySO.isDead = true;
                    audioManager.PlayEnemyDeathSound();
                    waveSO.deadEnemyCount++;
                }
                if (waveSO.waveState == WaveState.attacking && waveSO.ListOfEnemies.Count == waveSO.deadEnemyCount)
                {
                    Debug.Log("No enemies in-game, start next wave");
                    waveID++;
                    waveSO.waveState = WaveState.finished;
                    waveTimer.SetActive(true);
                    waveTimerScript.timerStart = true;
                }

            }
        }
    }

    public void CheckForEnemies()
    {
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            foreach (EnemySO enemySO in waveSO.ListOfEnemies)
            {
                if (enemySO.enemyPrefab == null)
                {
                    waveSO.deadEnemyCount = waveSO.ListOfEnemies.Count;
                }
            }
        }
    }

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

    

    [ContextMenu("Debug: Increase DeathCount")]
    public void DebugTestIncreaseDeathcount()
    {
        foreach (WaveSO waveSO in listOfAvailableWaves)
        {
            if (waveSO.waveState == WaveState.attacking)
            {
                waveSO.deadEnemyCount = waveSO.ListOfEnemies.Count;
                Debug.Log(waveSO.deadEnemyCount);
            }
        }
    }
}
