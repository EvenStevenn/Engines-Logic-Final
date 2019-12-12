using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyBehaviors : MonoBehaviour
{
    public GameObject turret;
    public GameObject enemy;
    public GameObject projectilePrefab;

    public EnemySO enemySO;

    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI countdown;

    public Vector3 enemyPos;
    public Vector3 turretPos;
    public Vector3 projectilePos;

    public int speed = 1;
    public int enemyHP;
    public int coinAmount = 1;

    public Transform shootFrom;
    public Rigidbody projectileRB;
    public float projectileSpeedHigh;
    public float projectileSpeedLow;

    //float nextShootTime;
    //Animator turrentAnim;



    //void Start()
    //{
    //    // TurretAnim = GetComponentInChildren<Animator>();
    //    //nextShootTime = 1f;
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        TurretAttack();
    //    }
    
    //    //projectilePrefab = GameObject.FindGameObjectWithTag("Projectile");
        

    //    //move the projectile to enemyPos
    //    if (projectilePrefab != null)
    //    {
    //        projectilePos = Vector3.MoveTowards(turret.transform.position, enemy.transform.position, 3);
    //    }

    //    ProjectileAttack();
    //}

    //void ProjectileAttack()
    //{
    //    float dist = Vector3.Distance(projectilePos, enemyPos);

    //    if (dist < 5)
    //    {
    //        Debug.Log("Projectile collided with enemy...");

    //        //damage
    //        if(enemyHP > 0)
    //        {
    //            enemyHP -= 10;
    //            Debug.Log("Enemy damaged... " + enemyHP);
    //        }

    //        //death
    //        else
    //        {
    //            Object.Destroy(enemy);
    //            Debug.Log("Enemy dead " + enemyHP);
    //            coinCount.text += 20;
    //            enemySO.isDead = true;
    //        }

    //        Object.Destroy(projectilePrefab);
    //    }
    //}

    //void TurretAttack()
    //{
    //    enemyPos = enemy.transform.position;
    //    turretPos = turret.transform.position;

    //    Instantiate(projectilePrefab, turretPos, Quaternion.identity);
    //    projectileRB = GetComponent<Rigidbody>();
    //    projectileRB.AddForce(new Vector3(Random.Range(projectileSpeedLow, projectileSpeedHigh), Random.Range(projectileSpeedLow, projectileSpeedHigh)), ForceMode.Impulse);
    //    //enemyPos.y = +1;
    //    projectilePrefab.transform.position = enemyPos;
    //    Debug.Log("Turret attacking...");
    //}
}
