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

    public TextMeshProUGUI coincount;
    public TextMeshProUGUI countdown;

    private Vector3 enemyPos;
    private Vector3 turretPos;
    private Vector3 projectilePos;

    private int speed = 1;
    private int enemyHP;
    private int coinAmount = 1;

    public Transform shootFrom;
    Rigidbody ProjectileRB;
    public float projectileSpeedHigh;
    public float projectileSpeedLow;

    //float nextShootTime;
    //Animator turrentAnim;



    void Start()
    {
        // TurretAnim = GetComponentInChildren<Animator>();
        //nextShootTime = 1f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TurretAttack();
        }
    
        //projectilePrefab = GameObject.FindGameObjectWithTag("Projectile");
        

        //move the projectile to enemyPos
        if (projectilePrefab != null)
        {
            projectilePos = Vector3.MoveTowards(turret.transform.position, enemy.transform.position, 3);
        }

        ProjectileAttack();
    }

    void ProjectileAttack()
    {
        float dist = Vector3.Distance(projectilePos, enemyPos);

        if (dist < 5)
        {
            Debug.Log("Projectile collided with enemy...");

            //damage
            if(enemyHP > 0)
            {
                enemyHP -= 10;
                Debug.Log("Enemy damaged... " + enemyHP);
            }

            //death
            else
            {
                Object.Destroy(enemy);
                Debug.Log("Enemy dead " + enemyHP);
                coincount.text += 20;
                enemySO.isDead = true;
            }

            Object.Destroy(projectilePrefab);
        }
    }

    void TurretAttack()
    {
        enemyPos = enemy.transform.position;
        turretPos = turret.transform.position;

        Instantiate(projectilePrefab, turretPos, Quaternion.identity);
        ProjectileRB = GetComponent<Rigidbody>();
        ProjectileRB.AddForce(new Vector3(Random.Range(projectileSpeedLow, projectileSpeedHigh), Random.Range(projectileSpeedLow, projectileSpeedHigh)), ForceMode.Impulse);
        //enemyPos.y = +1;
        projectilePrefab.transform.position = enemyPos;
        Debug.Log("Turret attacking...");
    }
}
