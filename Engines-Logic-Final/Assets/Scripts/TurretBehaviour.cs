using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public EnemyBehaviors eb;

    private void OnTriggerStay(Collider myCollider)
    {
        //turn turret to an enemy in range
        if(myCollider.tag == "Enemy")
        {
            gameObject.transform.rotation = Quaternion.LookRotation(myCollider.transform.position);
        }
    }
}
