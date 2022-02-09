using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

//using UnityEngine.InputSystem;

public class MoveToMouse : MonoBehaviour
{
    public float minimize = 0.03f;
    protected Camera mainCam;
    protected float z;
    protected Vector2 basePosition;

    
    protected virtual void Awake()
    {
        mainCam = Camera.main;
        var dist = transform.position - mainCam.transform.position;
        z = Distance(transform.position.z, mainCam.transform.position.z);
        basePosition = transform.position;
        
    }

    public void Move()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoveMouse(mousePos);
    }

    protected virtual void MoveMouse(Vector2 mousePos)
    {
        if (OutOfBounds()) return;

        mousePos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, z));
        Vector2 newPos = new Vector2(CalculateX(mousePos.x), CalculateY(mousePos.y));

        transform.position = new Vector3(basePosition.x, basePosition.y, 0) +
                             new Vector3(newPos.x, newPos.y, transform.position.z);
    }
    
    private bool OutOfBounds()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x > Screen.width || mousePos.y > Screen.height) return true;
        return false;
    }

    protected float CalculateX(float mouseX)
    {
        return mouseX * minimize * ((float) Screen.height / (float) Screen.width);
    }
    protected float CalculateY(float mouseY)
    {
        return mouseY * minimize * ((float) Screen.width / (float) Screen.height);
    }

    protected float Distance(float a, float b)
    {
        return Mathf.Abs(a - b);
    }
}
