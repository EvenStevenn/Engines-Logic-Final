using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform target;

    public float speed = 20f;

    public int attackDamage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Destroys bullet when target is gone
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Finds the direction of the buller to its target
        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }

        // Moves bullet towards enemy
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
    }


    // Assigns the bullet to a target
    public void AssignTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    // Destroys bullet on "collision"
    void HitTarget()
    {
        target.GetComponent<NPCMover>().TakeDamage(attackDamage);
        Destroy(gameObject);
        Debug.Log("Enemy hit");
    }
}
