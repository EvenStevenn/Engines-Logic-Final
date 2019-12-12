using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private void OnTriggerStay(Collider myCollider)
    {
        //turn turret to an enemy in range
        if(myCollider.tag == "Enemy")
        {
            gameObject.transform.rotation = Quaternion.LookRotation(myCollider.transform.position);
        }
    }

    public void DoAttack(Collider enemyCollider)
    {
        //spawn a projectile
        GameObject myPrefab = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        //fire at enemy
        Rigidbody rb = myPrefab.GetComponent<Rigidbody>();
        myPrefab.transform.position = Vector3.MoveTowards(myPrefab.transform.position, enemyCollider.transform.position, 3);
    }
}
