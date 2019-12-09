using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public List<ScriptableObject> listOfWaves;
    public Transform enemySpawnPoint;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginWave()
    {
        //find out which wave the player is currently on, by ID. 
        for(int i = 0; i <listOfWaves.Count;i++)
        {
            //uncompleted.
        }
    }



    
 
}
