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

    public float timeBetweenWaves = 10f;
    public float timeBetweenEnemies = 4f;

    

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
    }

    public void BeginWave()
    {
        foreach(WaveSO singleWave in listOfAvailableWaves)
        {
            if (waveSO.waveState == WaveState.available && waveSO.deadEnemyCount == 0)
            {
                waveSO.waveState = WaveState.attacking;

                foreach (EnemySO singleEnemy in waveSO.ListOfEnemies)
                {
                    Instantiate(enemySO.enemyPrefab);
                    enemySO.enemyPrefab.transform.position = enemySpawnPoint.position;
                    StartCoroutine(WaitTimeBetweenEnemies());
                }




                    
                    

                        

                    
               
            }
        }
    }



    
 
}
