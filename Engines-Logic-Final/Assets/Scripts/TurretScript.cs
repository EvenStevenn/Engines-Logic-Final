using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [Header("References for script's functionality")]
    public GameObject gameManager;
    public GameManager GM;
    public GameObject waveManager;
    public WaveManager waveManagerScript;
    public AudioManager audioManager;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public GameObject playerObj;
    public PlayerScript playerScript;



    [Header("Turret references")]
    public GameObject enemy;
    private Transform target;
    public Vector3 Distance;
    public GameObject turretHead;
    public Transform raycastDirection;
    public bool canShoot;

    [Header("Turret Attributes")]
    public float range = 5;
    public float rotationSpeed = 10f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Obtaining references to Player
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerScript>();

        // Obtaining references to WaveManager
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();


        // Obtaining references to GameManager 
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();

        // Call the update target functionality every 0.5 seconds
        InvokeRepeating("UpdateTurretTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (gameObject.transform.parent.CompareTag("TowerTile"))
        {
            canShoot = true;
            Debug.Log("Turret status, canShoot = " + canShoot);
        }
        else
        {
            canShoot = false;
            Debug.Log("Turret status, canShoot = " + canShoot);
        }

        // returns if there isn't a target
        if (target == null)
        {
            return;

        }

        // looks at closest target
        Vector3 dir = target.position - bulletSpawnPoint.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretHead.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        turretHead.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        // Raycasts to see if turret's bullet will hit an enemy
        Debug.DrawRay(bulletSpawnPoint.transform.position, (target.transform.position - bulletSpawnPoint.transform.position), Color.green);
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawnPoint.position, (target.transform.position - bulletSpawnPoint.transform.position), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Hit");
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
            }
        }

        // Turret shooting countdown
        fireCountdown -= Time.deltaTime;
        
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

    void OnTriggerStay(Collider other)
    {
        // checks for towertile before becoming a child of the collider
        if (playerScript.carryingTurret && Input.GetKeyDown(KeyCode.Mouse0) && other.gameObject.tag == "TowerTile")
        {
            if (other.gameObject.transform.childCount < 1)
            {
                gameObject.transform.SetParent(other.transform);
                gameObject.transform.position = other.transform.position + (new Vector3(0, 0.3f, 0));
                playerScript.carryingTurret = false;
            }
        }
    }

    // Spawns a bullet at the spawn position
    void Shoot()
    {
        if (canShoot)
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
}
