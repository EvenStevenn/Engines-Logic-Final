using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{ 
    public CharacterController controller;

    public float playerSpeed = 6.0f;
    public float playerSpeedCarrying = 2.0f;
    public float turnSmoothTime = .12f;
    public float groundDistance = 0.4f;
    public float gravity = -9.8f;
    float turnSmoothVelocity;

    public Transform cameraTransform;
    public Transform turretHolder;
    public Transform groundCheck;

    public GameObject turret;
    public Rigidbody turretRB;

    public bool hasTurret;
    bool isGrounded;

    public LayerMask groundMask;

    public Vector3 velocity;

    
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

        // will always move the player forward in both directions given the rotation of the player itself.
        Vector3 move = (this.gameObject.transform.forward * Mathf.Abs(input.x) * playerSpeed) + (this.gameObject.transform.forward * Mathf.Abs(input.y) * playerSpeed);
        controller.Move(move * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y< 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //code to change speed based on whether the player is carrying a turret
        //float speed = ((carrying) ? playerSpeedCarrying : playerSpeed)* inputDirection.magnitude;

        //old code:
        //transform.Translate(transform.forward * playerSpeed * inputDirection.magnitude * Time.deltaTime, Space.World);

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Turret")
        {
            hasTurret = true;
            turretRB.isKinematic = true;
            turret.transform.position = turretHolder.transform.position;
            turret.transform.rotation = turretHolder.transform.rotation;
            turret.transform.parent = turretHolder;
        }
    }
}
