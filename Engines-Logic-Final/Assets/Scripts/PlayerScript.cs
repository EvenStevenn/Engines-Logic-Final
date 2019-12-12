using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;

    public float playerSpeed = 2.0f;
    public float diagPlayerSpeed = 1.0f;
    public float straightPlayerSpeed = 2.0f;
    public float playerSpeedCarrying = 2.0f;
    public float turnSmoothTime = .12f;
    public float groundDistance = 0.4f;
    public float gravity = -9.8f;
    float turnSmoothVelocity;

    public Transform cameraTransform;
    public Transform groundCheck;

    public bool hasTurret;
    bool isGrounded;

    public LayerMask groundMask;

    public Vector3 velocity;


    void Start()
    {
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        hasTurret = false;
        float straightPlayerSpeed = playerSpeed;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;

        //rotates player based on input
        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        // will always move the player forward in both directions given the rotation of the player itself.
        Vector3 move = (this.gameObject.transform.forward * Mathf.Abs(input.x) * playerSpeed) + (this.gameObject.transform.forward * Mathf.Abs(input.y) * playerSpeed);
        controller.Move(move * Time.deltaTime);

        //gravity check based on "ground" layer
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        //checks if player is moving diagonally
        if (Input.GetAxisRaw("Horizontal") != 0 && (Input.GetAxisRaw("Vertical") != 0))
        {
            playerSpeed = diagPlayerSpeed;
            //Debug.Log(playerSpeed);
        }
        else
        {
            playerSpeed = straightPlayerSpeed;
            //Debug.Log(playerSpeed);
        }
    }
}
