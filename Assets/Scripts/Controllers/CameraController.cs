using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float FollowSpeed = 0.5f;

    [SerializeField]
    private float Distance = 15f;
    
    [SerializeField]
    private float Height = 1f;

    private Vector3 newpos = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate()
    {
        float newHeight = (target.position - transform.position).magnitude/Height;
        //print(newHeight);

        newpos = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * FollowSpeed);
        newpos.x = Distance;
        newpos.y = Math.Clamp(newHeight, 1f, Height);

        transform.position = newpos;
    }
}
