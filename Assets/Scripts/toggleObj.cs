using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleObj : MonoBehaviour
{
    public GameObject state1, state2;
    public Material hoverMat;
    [Range(0f, 100f)]
    public int thickness;
    
    private bool secondState = false;
    private SpriteRenderer spr;
    private Material defMat;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        defMat = GetComponent<SpriteRenderer>().material;
    }

    private void OnMouseEnter()
    {
        spr.material = hoverMat;
        spr.material.SetFloat("_Thicc", thickness);
    }

    private void OnMouseExit()
    {
        spr.material = defMat;
    }

    private void OnMouseDown()
    {
        if (secondState)
        {
            state1.SetActive(true);
            state2.SetActive(false);
            secondState = false;
        }
        else
        {
            state2.SetActive(true);
            state1.SetActive(false);
            secondState = true;
        }
    }
}
