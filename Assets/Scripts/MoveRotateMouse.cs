using System;
using UnityEngine;

public class MoveRotateMouse : MonoBehaviour
{
    public float minX = -2, minY = -2, maxX = 2, maxY = 2;
    public float minimize = 0.01f;
    private Camera mainCam;
    private float z;
    private Vector2 basePosition;

    private void Awake()
    {
        mainCam = Camera.main;
        var dist = transform.position - mainCam.transform.position;
        z = Distance(transform.position.z, mainCam.transform.position.z);
        basePosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var mousePos = Input.mousePosition;
        mousePos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, z));
        Vector2 newPos = new Vector2(CalculateX(mousePos.x), CalculateY(mousePos.y));

        transform.position = new Vector3(basePosition.x, basePosition.y, 0) +
                             new Vector3(newPos.x, newPos.y, transform.position.z);
        Rotate();
    }

    private void Rotate()
    {
        Vector2 bottomLeft = mainCam.ScreenToWorldPoint(new Vector3(0, 0, z));
        Vector2 topRight = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, z));
        float minPossibleX = CalculateX(bottomLeft.x), maxPossibleX = CalculateX(topRight.x);
        float minPossibleY = CalculateY(bottomLeft.y), maxPossibleY = CalculateY(topRight.y);

        float rotateX = Mathf.Lerp(minX, maxX,
            Distance(transform.position.x, minPossibleX) / Distance(maxPossibleX, minPossibleX));
        float rotateY = Mathf.Lerp(minY, maxY,
            Distance(transform.position.y, minPossibleY) / Distance(maxPossibleY, minPossibleY));

        transform.eulerAngles = new Vector3(rotateY, rotateX, 0);
    }

    private float CalculateX(float mouseX)
    {
        return mouseX * minimize * ((float) Screen.height / (float) Screen.width);
    }
    private float CalculateY(float mouseY)
    {
        return mouseY * minimize * ((float) Screen.width / (float) Screen.height);
    }

    private float Distance(float a, float b)
    {
        return Mathf.Abs(a - b);
    }
}
