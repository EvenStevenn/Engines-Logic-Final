using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveState
{
    available,
    attacking,
    finished
}
public class WaveManager : MonoBehaviour
{

    public List<ScriptableObject> listOfAvailableWaves;
    public Transform enemySpawnPoint;
    public float timeBetweenWaves = 10f;

 




    void Update()
    {

    }

    public void BeginWave()
    {
        for(int i = 0; i <listOfAvailableWaves.Count;i++)
        {
            if (listOfAvailableWaves[i])
            {
                ;
            }
        }
    }



    
 
}
