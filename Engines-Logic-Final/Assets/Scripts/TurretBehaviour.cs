using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [Header("References for script's functionality")]
    public GameObject gameManager;
    public GameManager GM;
    public GameObject waveManager;
    public WaveManager waveManagerScript;
    public AudioManager audioManager;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    [Header("Turret references")]
    public GameObject enemy;
    private Transform target;
    public Vector3 Distance;

    [Header("Turret Attributes")]
    public float range = 5;
    public float rotationSpeed = 10f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        //returns if there isn't a target
        if (target == null)
        {
            return;
        }

        //looks at closest target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation =  Quaternion.Euler(rotation);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    //audioManager.PlayEnemyDeathSound();           
    }

    //checks and determines the enemy closest to turret
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

        void Shoot()
        {
            Debug.Log("Shoot");
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
}
