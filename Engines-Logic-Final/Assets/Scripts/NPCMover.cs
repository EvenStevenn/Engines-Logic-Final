using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    public GameObject waypoint0;
    
    public Node targetNode;

    public string enemyName;

    public int enemySpeed;
    public int enemyHealth;
    public int numOfCoins;

    public bool isDead;
    public bool reachedEnd;


    public GameObject gameWinMenu;
    public GameObject gameOverMenu;
    public Camera mainCam;

    public GameObject gameManager;
    public GameManager GM;
    public GameObject waveManager;
    public WaveManager waveManagerScript;

    private void Start()
    {
        waypoint0 = GameObject.FindGameObjectWithTag("waypoint0");
        targetNode = waypoint0.GetComponent<Node>();
        gameOverMenu = GameObject.FindGameObjectWithTag("LoseScreen");

        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        float step = enemySpeed * Time.deltaTime;


        if(targetNode == null)
        {
            //Time.timeScale = 0f;
            //gameOverMenu.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;
            //Debug.Log("Level failed. You lose!");
            //mainCam.enabled = false;
            Debug.Log("target node is null");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, step);
            transform.LookAt(targetNode.transform);
            float distance = Vector3.Distance(transform.position, targetNode.transform.position);
            if (distance == 0f)
            {
                int newPos = Random.Range(0, targetNode.neighbours.Count);

                // Find a new random position and go to it from your list of neighbors. 
                targetNode = targetNode.neighbours[newPos];
            }

        }

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            GM.deadEnemyCount++;
        }

    }

    public void TakeDamage (int attackValue)
    {
        enemyHealth -= attackValue;
        Debug.Log("Enemy hit, its health is now " + enemyHealth);
    }
}