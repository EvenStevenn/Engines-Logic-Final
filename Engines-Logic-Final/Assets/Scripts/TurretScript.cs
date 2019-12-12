using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    bool carryingTurret;
    public GameObject playerTurretHolder;

    // Start is called before the first frame update
    void Start()
    {
        playerTurretHolder = GameObject.FindGameObjectWithTag("PlayerTurretHolder");
        
        
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
            this.gameObject.transform.position = playerTurretHolder.transform.position;
            this.gameObject.transform.rotation = playerTurretHolder.transform.rotation;
            this.gameObject.transform.parent = playerTurretHolder.transform;
            Debug.Log("player has picked up turret");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && other.gameObject.tag == "TowerTile")
        {
            if (other.gameObject.transform.childCount < 1)
            {
                this.gameObject.transform.parent = other.transform;
                this.gameObject.transform.position = other.transform.position + (new Vector3(0, 0.3f, 0));
                carryingTurret = false;
            }
        }

    }
}
