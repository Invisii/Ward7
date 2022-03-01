using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class POIScript : MonoBehaviour
{
    public TextAsset inkAsset;
    public Material hoverMat;
    [Range(0f, .99999f)]
    public float thickness; 
        
    private Material defMat;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        defMat = GetComponent<SpriteRenderer>().material;
        if (hoverMat == null)
        {
            hoverMat = Resources.Load<Material>("/Assets/Renderer/Shine_Material.mat");
        }
    }

    private void OnMouseEnter()
    {
        if (!InteractionManagerScript.S.focused)
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
        if (InteractionManagerScript.S.focused)
        {
            InteractionManagerScript.S.unFocusObj();
        }
        else if (!InteractionManagerScript.S.activeStory)
        {
            InteractionManagerScript.S.interaction = new Story(inkAsset.text); //make Unity obj out of our Ink file
            InteractionManagerScript.S.BeginDialogue();   
        }
    }

}
