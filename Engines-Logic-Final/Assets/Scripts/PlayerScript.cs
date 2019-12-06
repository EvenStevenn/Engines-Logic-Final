using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{ 
    public float playerSpeed = 6.0f;
    public float playerSpeedCarrying = 2.0f;

    public float turnSmoothTime = .12f;
    float turnSmoothVelocity;

    public Transform cameraTransform;
    public Transform turretHolder;

    public GameObject turret;
    public Rigidbody turretRB;

    public bool hasTurret;

    
    void Start()
    {
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        hasTurret = false;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;

        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        //bool carrying;

        //float speed = ((carrying) ? playerSpeedCarrying : playerSpeed)* inputDirection.magnitude;

        transform.Translate(transform.forward * playerSpeed * inputDirection.magnitude * Time.deltaTime, Space.World);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            hasTurret = true;
            turretRB.isKinematic = true;
            turret.transform.position = turretHolder.transform.position;
            turret.transform.rotation = turretHolder.transform.rotation;
            turret.transform.parent = turretHolder;
        }
    }
}
