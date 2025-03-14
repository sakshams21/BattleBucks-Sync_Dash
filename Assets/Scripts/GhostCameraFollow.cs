using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GhostCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // The object the camera follows
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from target
    public float smoothSpeed = 5f; // Speed of following

    void LateUpdate()
    {
        //if (target == null) return;


        // Vector3 desiredPosition = target.position + offset;

        transform.position = target.position + offset;

        transform.LookAt(target);
    }


}
