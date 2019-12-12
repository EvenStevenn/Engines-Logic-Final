using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public GameObject bulletPrefab;
    public Rigidbody bulletRB;
    public GameObject enemy;
    public Vector3 enemyPos;
    public bool canShoot;

    private void Start()
    {
        bulletPrefab = GameObject.FindGameObjectWithTag("Bullet");
        bulletRB = bulletPrefab.GetComponent<Rigidbody>();
        canShoot = true;
    }

    private void Update()
    {
        Vector3.MoveTowards(bulletPrefab.transform.position, enemyPos, 3*Time.deltaTime);
    }

    private void OnTriggerStay(Collider myCollider)
    {
        //turn turret to an enemy in range
        if (myCollider.tag == "Enemy" && canShoot)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(myCollider.transform.position - this.gameObject.transform.position);
            StartCoroutine(DoAttack((myCollider)));
            enemy.transform.position = myCollider.transform.position;
            enemyPos = myCollider.transform.position;
        }
    }
    
    public IEnumerator DoAttack (Collider enemyCollider)
    {
        bulletPrefab = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        canShoot = false;
        yield return new WaitForSeconds(1.0f);
        canShoot = true;
    }
}
