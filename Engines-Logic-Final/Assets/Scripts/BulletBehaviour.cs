using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject targetEnemy;
    public GameObject turret;
    public TurretBehaviour turretScript;
    // Start is called before the first frame update
    void Start()
    {
        turret = GameObject.FindGameObjectWithTag("Turret");
        turretScript = turret.GetComponent<TurretBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.MoveTowards(this.gameObject.transform.position, turretScript.enemyPos, 3 * Time.deltaTime);
    }


}
