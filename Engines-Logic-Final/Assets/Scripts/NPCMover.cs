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

    private void Start()
    {
        waypoint0 = GameObject.FindGameObjectWithTag("waypoint0");
        targetNode = waypoint0.GetComponent<Node>();
        gameOverMenu = GameObject.FindGameObjectWithTag("LoseScreen");
        
              
        //if(enemySO.name == "RunningEnemy")
        //{
        //    speed = enemySO.enemySpeed;
        //}
        //if(enemySO.name == "WalkingEnemy")
        //{
        //    speed = enemySO.enemySpeed;
        //}
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
                //find a new random position and go to it from your list of neighbors. 
                targetNode = targetNode.neighbours[newPos];
            }

        }

    }
}