using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public EnemyBehaviors eb;

    private void OnTriggerStay(Collider other)
    {
        //initiate attack sequence if enemy is detected
        if (other.tag == "Enemy")
        {
            TurretAttack();
        }

        //move the projectile to enemyPos
        if (eb.projectilePrefab != null)
        {
            eb.projectilePos = Vector3.MoveTowards(eb.turret.transform.position, eb.enemy.transform.position, 3);
        }

        ProjectileAttack();
    }

    void ProjectileAttack()
    {

        float dist = Vector3.Distance(eb.projectilePos, eb.enemyPos);

        if (dist < 5)
        {
            Debug.Log("Projectile collided with enemy...");

            //damage
            if (eb.enemyHP > 0)
            {
                eb.enemyHP -= 10;
                Debug.Log("Enemy damaged... " + eb.enemyHP);
            }

            //death
            else
            {
                Object.Destroy(eb.enemy);
                Debug.Log("Enemy dead " + eb.enemyHP);
                eb.coinCount.text += 20;
                eb.enemySO.isDead = true;
            }

            Object.Destroy(eb.projectilePrefab);
        }
    }

    void TurretAttack()
    {
        eb.enemyPos = eb.enemy.transform.position;
        eb.turretPos = eb.turret.transform.position;

        Instantiate(eb.projectilePrefab, eb.turretPos, Quaternion.identity);
        eb.projectileRB = GetComponent<Rigidbody>();
        eb.projectileRB.AddForce(new Vector3(Random.Range(eb.projectileSpeedLow, eb.projectileSpeedHigh), Random.Range(eb.projectileSpeedLow, eb.projectileSpeedHigh)), ForceMode.Impulse);
        eb.projectilePrefab.transform.position = eb.enemyPos;
        Debug.Log("Turret attacking...");
    }
}
