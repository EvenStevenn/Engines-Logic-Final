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

    public float timeBetweenWaves = 10f;
    public float timeBetweenEnemies = 4f;

    private void Start()
    {
        BeginWave();
    }



    void Update()
    {
       
    }

    public IEnumerator WaitTimeBetweenEnemies()
    {
        yield return new WaitForSeconds(timeBetweenEnemies);
    }

    public IEnumerator WaitTimeBetweenWaves()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        countdownTimerText.text = timeBetweenWaves.ToString();
        //Mathf.RoundToInt(Mathf.Ceil(currentTimeValue)).ToString();

    }

    public void BeginWave()
    {
        foreach(WaveSO singleWave in listOfAvailableWaves)
        {
            if (singleWave.waveState == WaveState.available && singleWave.deadEnemyCount == 0)
            {
                singleWave.waveState = WaveState.attacking;

                foreach (EnemySO singleEnemy in singleWave.ListOfEnemies)
                {
                    if(singleWave.waveState == WaveState.attacking)
                    {
                    Instantiate(singleEnemy.enemyPrefab);
                    singleEnemy.enemyPrefab.transform.position = enemySpawnPoint.position;
                    singleWave.deadEnemyCount++;
                    StartCoroutine(WaitTimeBetweenEnemies());
                    }

                    if(singleWave.deadEnemyCount == singleWave.ListOfEnemies.Count)
                    {
                        singleWave.waveState = WaveState.finished;
                    }
                }
                if(singleWave.waveState == WaveState.finished)
                {
                    StartCoroutine(WaitTimeBetweenWaves());
                    
                    //call timer function
                }




                    
                    

                        

                    
               
            }
        }
    }



    
 
}
