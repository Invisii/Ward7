using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hallwayMove : MonoBehaviour
{
    private float minDistance = 0;
    private float maxDistance = 25;
    private float moveSpeed = 0.08f;
    private GameObject target;
    private Vector3 pos;

    private void Awake()
    {
        target = GameObject.Find("Mouse Target");
        pos = target.transform.position;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (target.transform.position.z < maxDistance)
            {
                var newPos = new Vector3(pos.x, pos.y, pos.z + moveSpeed);
                target.transform.position = newPos;
                pos = newPos;
            }
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (target.transform.position.z > minDistance)
            {
                var newPos = new Vector3(pos.x, pos.y, pos.z - moveSpeed*2);
                target.transform.position = newPos;
                pos = newPos;
            }
        }
    }
}
