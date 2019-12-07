using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float yaw;
    private float pitch;
    public float cameraDistance = 2.0f;
    public float mouseSensitivity = 10.0f;
    

    public Vector2 pitchMinMax = new Vector2 (-40,85);

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        
        Vector3 playerRotation = new Vector3(pitch, yaw);
        transform.eulerAngles =playerRotation;

        transform.position = player.position - transform.forward * cameraDistance ;
        
    }
}
