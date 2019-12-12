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
          //bulletRB = bulletPrefab.GetComponent<Rigidbody>();
        
    }

    private void Update()
    {


    }

    private void OnTriggerStay(Collider other)
    {
        //turn turret to an enemy in range
        if (other.tag == "Enemy")
        {
            gameObject.transform.rotation = Quaternion.LookRotation(other.transform.position - this.gameObject.transform.position);
            enemyPos = other.transform.position;
            StartCoroutine(DoAttack((other)));
        }
    }
    
    public IEnumerator DoAttack (Collider enemyCollider)
    {
        bulletPrefab = Instantiate(bulletPrefab, projectileSpawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(1.0f);
        
    }
}
