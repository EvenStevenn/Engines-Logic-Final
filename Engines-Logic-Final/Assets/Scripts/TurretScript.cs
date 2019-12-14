using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [Header("Turret's References for Carrying")]
    bool carryingTurret;
    public GameObject playerTurretHolder;

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
    public GameObject turretHead;

    [Header("Turret Attributes")]
    public float range = 5;
    public float rotationSpeed = 10f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Start-up for turret pick-up functionality
        carryingTurret = false;
        playerTurretHolder = GameObject.FindGameObjectWithTag("PlayerTurretHolder");
        
        // Obtaining references to wavemanager and gamemanager scripts
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();

        // Call the update target functionality every 0.5 seconds
        InvokeRepeating("UpdateTurretTarget", 0f, 0.5f);

    }

    private void Update()
    {
        // returns if there isn't a target
        if (target == null)
        {
            return;
        }

        // looks at closest target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretHead.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        turretHead.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

        //audioManager.PlayEnemyDeathSound();           
    }

    // Checks for player to be within radius before being picked up
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

        // checks for towertile before becoming a child of the collider
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

    // Finds the closest target based on distance to turret
    void UpdateTurretTarget()
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

    // Spawns a bullet at the spawn position
    void Shoot()
    {
        Debug.Log("Shoot");
        GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        if (bullet != null)
        {
            bulletScript.AssignTarget(target);
        }
    }
}
