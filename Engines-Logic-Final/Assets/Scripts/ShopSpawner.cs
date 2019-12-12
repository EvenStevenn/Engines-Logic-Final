using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    public GameObject turret;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("pressed e");
                GameObject myTurret = Instantiate<GameObject>(turret, other.transform);
                float myY = myTurret.transform.position.y;
                myY += 0.5f;
                myTurret.transform.Translate(other.transform.position.x, myY, other.transform.position.z);
            }
        }
    }
}
