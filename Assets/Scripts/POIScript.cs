using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class POIScript : MonoBehaviour
{
    public TextAsset inkAsset;
    public Material hoverMat;
    [Range(0f, 200f)]
    public float thickness;

    public bool clicked = false;
    public Sprite color;
        
    private Material defMat;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        defMat = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (clicked)
        {
            spr.sprite = color;
        }
    }

    private void OnMouseEnter()
    {
        if (!InteractionManagerScript.S.focused && !InteractionManagerScript.S.activeStory)
        {
            spr.material = hoverMat;
            spr.material.SetFloat("_Thicc", thickness);
        }
    }

    private void OnMouseExit()
    {
        if (!InteractionManagerScript.S.focused)
        {
            spr.material = defMat;
        }
    }

    private void OnMouseDown()
    {
        if (!InteractionManagerScript.S.focused && !InteractionManagerScript.S.activeStory)
        {
            InteractionManagerScript.S.interaction = new Story(inkAsset.text); //make Unity obj out of our Ink file
            InteractionManagerScript.S.BeginDialogue();   
        }

        clicked = true;
    }

}
