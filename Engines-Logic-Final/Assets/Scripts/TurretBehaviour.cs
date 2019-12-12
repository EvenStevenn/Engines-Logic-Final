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
<<<<<<< HEAD
    

<<<<<<< HEAD
<<<<<<< HEAD
        //move the projectile to enemyPos
        /*if (eb.projectilePrefab != null)
        {
            eb.projectilePos = Vector3.MoveTowards(eb.turret.transform.position, eb.enemy.transform.position, 3);
        }

        ProjectileAttack();*/
    }

    void ProjectileAttack()
=======
=======
>>>>>>> parent of fcd6ea9... Added turret turning
    public void DoAttack(Collider enemyCollider)
>>>>>>> parent of fcd6ea9... Added turret turning
=======
    }

    public void DoAttack(Collider enemyCollider)
>>>>>>> parent of eab6e3b... Fixed wave manager, added timer with funcitonality in UI
    {
        //spawn a projectile
        GameObject myPrefab = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        //fire at enemy
        Rigidbody rb = myPrefab.GetComponent<Rigidbody>();
        myPrefab.transform.position = Vector3.MoveTowards(myPrefab.transform.position, enemyCollider.transform.position, 3);
    }


}
