using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject turret;
    bool carryingTurret;
    Rigidbody turretRB;
    public GameObject turretHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Player")
        {
            carryingTurret = true;
            turretRB.isKinematic = true;
            turret.transform.position = turretHolder.transform.position;
            turret.transform.rotation = turretHolder.transform.rotation;
            turret.transform.parent = turretHolder.transform;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            turret.transform.parent = null;
            carryingTurret = false;
            /*if (other.gameObject.tag == "TowerTile")
            {
                //this.gameObject
            }*/
        }

    }
}
