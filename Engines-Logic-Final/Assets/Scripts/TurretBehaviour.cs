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

    public GameObject gameManager;
    public GameManager GM;

    public AudioManager audioManager;

    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast (this.gameObject.transform.position, enemyPos, out hit))
        {
            Debug.Log("EH");
            Destroy(enemy);
            Debug.Log(GM.deadEnemyCount);
            audioManager.PlayEnemyDeathSound();           
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
            GM.deadEnemyCount++;
            //StartCoroutine(DoAttack((other)));
        }
    }

    public IEnumerator DoAttack(Collider enemyCollider)
    {
        
        yield return new WaitForSeconds(1.0f);

    }
}
