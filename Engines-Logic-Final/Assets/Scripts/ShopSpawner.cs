using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSpawner : MonoBehaviour
{
    [Header("Variable reference")]
    public bool canSpawn = false;

    [Header("Manual references")]
    public GameObject turret;
    public Transform turretSpawnPoint;
    public TextMeshProUGUI shopDisplay;

    [Header("Automatic References")]
    GameObject gameManager;
    GameManager GM;

    public int turretCost = 10;

    public void Start()
    {
        // Obtaining references to GameManager 
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();

    }

    public void Update()
    {
        //check if player wants to buy turret
        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("pressed e");

            //check if player is within proper range
            if (canSpawn && GM.playerCurrency >= turretCost)
            {
                SpawnTurret();
                shopDisplay.text = null;
            }
            else
            {
                shopDisplay.text = ("Not Enough Coins");
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //flag grants ability to buy turrets when in shop range
            Debug.Log("within range of shop");
            canSpawn = true;
        }
    }
       
    void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Turret"))
        {
            canSpawn = false;
            Debug.Log("There's a turret in the spawn zone");
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
        if (other.CompareTag("Turret"))
        {
            canSpawn = true;
            Debug.Log("Turret is no longer in the spawn zone");
        }
    }


    public void SpawnTurret()
    {
        //instantiate a new turret at its spawnpoint
        Instantiate(turret, turretSpawnPoint.transform.position, Quaternion.identity);
        GM.playerCurrency -= turretCost;
        shopDisplay.text = GM.playerCurrency.ToString();
    }
}
