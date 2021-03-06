﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Variables for mouse movement")]
    private float yaw;
    private float pitch;
    public Transform player;

    [Header("Adjustable Attributes")]
    public float cameraDistance = 2.0f;
    public float mouseSensitivity = 10.0f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);




    // Update is called once per frame: Will obtain mouse input and move the camera based on move movement
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        Vector3 playerRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = playerRotation;

        transform.position = player.position - transform.forward * cameraDistance;

    }

    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;

        [HideInInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera;

        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }


        /*                                                                                                                    Segment of code that was suppose to raycast to the near camera nearclip points, detect collision/oclusion, and move the camera forward based on collision distance
               public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
               {
                   if (!camera)
                       return;

                   //clear the contents of intoArray
                   intoArray = new Vector3[5];

                   float z = camera.nearClipPlane;
                   float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z;
                   float y = x / camera.aspect;

                   //top left 
                   intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; //added and rotated the point relative to camera
                   //top right
                   intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition;
                   //bottom left
                   intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition;
                   //bottom right
                   intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition;
                   //camera's position
                   intoArray[4] = cameraPosition - camera.transform.forward;
               }

               bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
               {
                   for (int i = 0; i < clipPoints.Length; i++)
                   {
                       Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                       float distance = Vector3.Distance(clipPoints[i], fromPosition);
                       if (Physics.Raycast(ray, distance, collisionLayer))
                       {
                           return true;
                       }
                   }
                   return false;
               }


               public float GetAdjustedDistanceWithRayFrom(Vector3 from)
               {
                   float distance = -1;

                   for (int i = 0; i < desiredCameraClipPoints.Length; i++)
                   {
                       Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                       RaycastHit hit;
                       if (Physics.Raycast(ray, out hit))
                       {
                           if (distance == -1)
                               distance = hit.distance;
                           else
                           {
                               if (hit.distance < distance)
                                   distance = hit.distance;
                           }
                       }
                   }
                   if (distance == -1)
                       return 0;
                   else
                       return distance;
               }

               public void CheckColliding(Vector3 targetPosition)
               {
                   if (CollisionDetectedAtClipPoints (desiredCameraClipPoints, targetPosition))
                   {
                       colliding = true;
                   }
                   else
                   {
                       colliding = false;
                   }
               }

           }
       */
    }
}
