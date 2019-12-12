using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject bulletPrefab;
    public Rigidbody bulletRB;
    public GameObject enemy;
    public Vector3 enemyPos;



    private void Start()
    {
        //bulletPrefab = GameObject.FindGameObjectWithTag("Bullet");

    }

    private void Update()
    {

    Vector3.MoveTowards(bulletPrefab.transform.position, enemyPos, 3*Time.deltaTime);
    }

    private void OnTriggerStay(Collider myCollider)
    {
        //turn turret to an enemy in range
        if (myCollider.CompareTag("Enemy"))
        {
            gameObject.transform.rotation = Quaternion.LookRotation(myCollider.transform.position - this.gameObject.transform.position);
            StartCoroutine(DoAttack((myCollider)));
            enemy.transform.position = myCollider.transform.position;
            enemyPos = myCollider.transform.position;
            bulletRB = bulletPrefab.GetComponent<Rigidbody>();
        }
    }
    
    public IEnumerator DoAttack (Collider enemyCollider)
    {
        bulletPrefab = Instantiate(bulletPrefab, projectileSpawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(1.0f);
        
    }
}
