using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    public GameObject turret;
    public Transform turretSpawnPoint;
    public bool canSpawn = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //flag grants ability to buy turrets when in shop range
            Debug.Log("within range of shop");
            canSpawn = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //flag removes ability to buy turrets when out of shop range
            Debug.Log("out of shop range");
            canSpawn = false;
        }
    }

    public void Update()
    {
        //check if player wants to buy turret
        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("pressed e");

            //check if player is within proper range
            if (canSpawn)
            {
                SpawnTurret();
            }
        }
    }

    public void SpawnTurret()
    {
        //instantiate a new turret at its spawnpoint
        Instantiate(turret, turretSpawnPoint.transform.position, Quaternion.identity);
    }
}
