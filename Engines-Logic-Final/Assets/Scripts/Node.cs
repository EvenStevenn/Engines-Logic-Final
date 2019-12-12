using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbours;
    //public bool occupied;

    private void OnDrawGizmosSelected()
    {
        foreach (Node singleNode in neighbours)
        {
            //Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, singleNode.transform.position);
        }

    }
}
