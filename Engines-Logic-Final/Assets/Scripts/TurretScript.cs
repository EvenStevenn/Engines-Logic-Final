using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    bool carryingTurret;
    Rigidbody turretRB;
    public GameObject turretHolder;
    public GameObject towerTile;

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
            this.gameObject.transform.position = turretHolder.transform.position;
            this.gameObject.transform.rotation = turretHolder.transform.rotation;
            this.gameObject.transform.parent = turretHolder.transform;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && other.gameObject.tag == "TowerTile")
        {
            if (other.gameObject.transform.childCount < 1)
            {
                this.gameObject.transform.parent = towerTile.transform;
                this.gameObject.transform.position = towerTile.transform.position + (new Vector3(0, 0.5f, 0));
                carryingTurret = false;
            }
        }

    }
}
