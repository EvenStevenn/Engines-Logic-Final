using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int enemySpeed;
    public int enemyHealth;
    public int numOfCoins;

    [Header("Manual references")]
    public GameObject gameWinMenu;
    public GameObject gameOverMenu;
    public Camera mainCam;

    [Header("Automatic References")]
    public GameObject waypoint0;
    public Node targetNode;
    public GameObject gameManager;
    public GameManager GM;
    public GameObject waveManager;
    public WaveManager WM;
    public GameObject audioManager;
    public AudioManager AM;

    private void Start()
    {
        // Gets refrence to the first node that the enemy needs to move to
        waypoint0 = GameObject.FindGameObjectWithTag("waypoint0");
        targetNode = waypoint0.GetComponent<Node>();

        // Gets reference to the WaveManager
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        WM = waveManager.GetComponent<WaveManager>();

        // Gets reference to GameManager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();


        // Gets reference to AudioManager
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        AM = audioManager.GetComponent<AudioManager>();
    }

    // Moves the enemy towards its neighbour node
    private void Update()
    {
        float step = enemySpeed * Time.deltaTime;

        // Destroys the gameobject once it hits the final node, decreases the life counter, and increases the enemy deathcount
        if(targetNode == null)
        {
            Destroy(gameObject);
            GM.lives--;
            GM.deadEnemyCount++;
            Debug.Log(GM.lives);

            // Checks if the number of lives has been reduced to zero and calls the game over screen if so
            if (GM.lives < 1)
            {
                GM.LossScreen();
            }
        }
        // Else it will move towards its neighbour and generate a new neighbour hitting its node
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

        // Code to resolve the enemies death when hit by turret enough times
        if (enemyHealth <= 0)
        {
            // Deletes the enemy
            Destroy(gameObject);

            // Increase the GameManger's death count
            GM.deadEnemyCount++;

            // Checks the conditions for the wave to end
            WM.WaveStatusCheck();

            // Plays the death sound of enemy
            AM.PlayEnemyDeathSound();

            // Increase the player's currency count
            GM.playerCurrency += numOfCoins;
            GM.UpdatePlayerCurrencyCount();
        }

    }

    // Code for the bulletPrefab to call when hitting the target
    public void TakeDamage (int attackValue)
    {
        enemyHealth -= attackValue;
        Debug.Log("Enemy hit, its health is now " + enemyHealth);
    }
}