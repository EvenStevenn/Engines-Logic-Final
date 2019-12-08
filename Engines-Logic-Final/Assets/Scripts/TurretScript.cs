using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject turret;

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
        if (Input.GetKey(KeyCode.F) && other.gameObject.tag == "TowerTile")
        {
            turret.transform.parent = null;
            
        }
    }
}
