using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject forRotation;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;

    public void OnTriggerEnter(Collider myCollider)
    {
        //check if what entered is an enemy
        if (myCollider.tag == "Enemy")
        {
            DoAttack(myCollider);
        }
    }

    public void DoAttack(Collider enemyCollider)
    {
        //spawn a projectile
        GameObject myPrefab = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Debug.Log("Bullet spawned");

        //fire at enemy
        Rigidbody rb = myPrefab.GetComponent<Rigidbody>();
        myPrefab.transform.position = Vector3.MoveTowards(myPrefab.transform.position, enemyCollider.transform.position, 3);
    }


}
