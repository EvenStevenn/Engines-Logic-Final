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
            Debug.Log("within range of shop");
            canSpawn = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("out of shop range");
            canSpawn = false;
        }
    }

    public void Update()
    {
        if (canSpawn && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("pressed e");
            SpawnTurret();
        }
    }

    public void SpawnTurret()
    {
        GameObject myTurret = Instantiate<GameObject>(turret, turretSpawnPoint);
        //GameObject myTurret = Instantiate<GameObject>(turret, other.transform);
        //float myY = myTurret.transform.position.y;
        //myY += 0.5f;
        //myTurret.transform.Translate(other.transform.position.x, myY, other.transform.position.z);
    }
}
