using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public Vector3 enemyPos;
    public Vector3 targetPos;
    public GameObject enemy;
    float range = 5;
    public GameObject waveManager;
    public WaveManager waveManagerScript;
    public AudioManager audioManager;


    private void Awake()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast (this.gameObject.transform.position, enemyPos, out hit))
        {
            Debug.Log("EH");
            Destroy(enemy);
            audioManager.PlayEnemyDeathSound();
            waveManagerScript.CheckForEnemies();
            
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //turn turret to an enemy in range
        if (other.tag == "Enemy")
        {
            gameObject.transform.rotation = Quaternion.LookRotation(other.transform.position - this.gameObject.transform.position);
            targetPos = this.gameObject.transform.position - other.gameObject.transform.position;
            enemyPos = other.gameObject.transform.position;
            enemy = other.gameObject;
            //StartCoroutine(DoAttack((other)));
        }
    }

    public IEnumerator DoAttack(Collider enemyCollider)
    {
        
        yield return new WaitForSeconds(1.0f);

    }
}
