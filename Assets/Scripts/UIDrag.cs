using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour
{

    private float minY = 1885;
    private float maxY;
    private RectTransform ribbon;

    private void Start()
    {
        maxY = transform.position.y;
        ribbon = (RectTransform) transform;
    }
    
    public void onDrag()
    {
        float mouseY = Input.mousePosition.y + ribbon.rect.height/2;
        if (mouseY < maxY && mouseY > minY)
        {
            Vector3 pos = this.transform.position;
            var newPos = new Vector3(pos.x, mouseY, pos.z);
            transform.position = newPos;
        }
    }

}
