using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    public GameObject waypoint0;
    public Node targetNode;
    public float speed = 3f;

    private void Start()
    {
        waypoint0 = GameObject.FindGameObjectWithTag("waypoint0");
        targetNode = waypoint0.GetComponent<Node>();
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
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