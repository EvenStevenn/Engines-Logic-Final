using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    public GameObject waypoint0;
    public Node targetNode;
    public int speed = 3;

    public EnemySO runningEnemy;
    public EnemySO walkingEnemy;

    public GameObject gamewinMenu;
    public GameObject gameoverMenu;
    public Camera mainCam;

    private void Start()
    {
        waypoint0 = GameObject.FindGameObjectWithTag("waypoint0");
        targetNode = waypoint0.GetComponent<Node>();
        if(runningEnemy.name == "Running Enemy")
        {
            speed = runningEnemy.enemySpeed;
        }
        if(walkingEnemy.name == "Walking Enemy")
        {
            speed = walkingEnemy.enemySpeed;
        }
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        if(targetNode == null)
        {
            Time.timeScale = 0f;
            //gameoverMenu = Get
            gameoverMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Level failed. You lose!");
            mainCam.enabled = false;
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